using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PRJ_MazeWinForms.Authentication
{

    public class User
    {
        public int PlayerId { get; }
        public string Username { get; }
        public string PasswordHash { get; }

        public User(int id, string username, string passwordHash)
        {
            PlayerId = id;
            Username = username;
            PasswordHash = passwordHash;
        }
    }

    public class Game
    {
        public int GameId { get; }
        public int PlayerId { get; }
        public int Score { get; }
        public string StoredSignature { get; }

        public Game(int gameId, int playerId, int score, string storedSig)
        {
            GameId = gameId;
            PlayerId = playerId;
            Score = score;
            StoredSignature = storedSig;
        }
    }


    public class DatabaseHelper
    {
        private const string _databaseName = "MazeDatabase.mdf";
        private string _directory;
        private string _connectionString;
        public DatabaseHelper()
        {
            _directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyMazeProgram";
            Directory.CreateDirectory(_directory);
            _connectionString = @"Provider = Microsoft Jet 4.0 OLE DB Provider;Data Source = " + _directory + @"\" + _databaseName + ";";
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            if (!File.Exists(_directory + @"\" + _databaseName))
            {
                CatalogClass cat = new CatalogClass();
                cat.Create(_connectionString);
                cat = null;
                CreateTables();
                LogHelper.Log("Created Database successfully");
            }
            else
            {
                LogHelper.ErrorLog("Database already exists");
            }
        }
        private bool CreateTables()
        {
            bool success = true;
            // User table
            string userTableCreation =
                "CREATE TABLE [UserDatabase] ("
                + "[PlayerId] INT NOT NULL,"
                + "[Username] VARCHAR(13) NOT NULL,"
                + "[PasswordHash] VARCHAR(64) NOT NULL,"
                + "PRIMARY KEY(PlayerId)"
                + ");";

            // Score table
            string scoreTableCreation =
                "CREATE TABLE [ScoreDatabase] ("
                + "[GameId] INT NOT NULL,"
                + "[PlayerId] INT NOT NULL,"
                + "[Score] INT NOT NULL,"
                + " [Signature] VARCHAR(64),"
                + "PRIMARY KEY(GameId),"
                + "FOREIGN KEY(PlayerId) REFERENCES UserDatabase(PlayerId)"
                + ");";

            if (SqlNonQuery(userTableCreation))
            {
                LogHelper.Log("User Table Created");
            }
            else
            {
                success = false;
            }

            if (SqlNonQuery(scoreTableCreation))
            {
                LogHelper.Log("Score Table Created");
            }
            else
            {
                success = false;
            }
            return success;
        }

        private bool SqlNonQuery(string commandText, params object[] parameters)
        {
            bool success = true;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(commandText))
                {
                    command.Connection = connection;
                    foreach (object param in parameters)
                    {
                        command.Parameters.Add(new OleDbParameter("?", OleDbType.BSTR)).Value = param;
                    }
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                        success = false;
                    }
                }
            }
            return success;
        }

        private object SqlScalarQuery(string commandText, params object[] parameters)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(commandText))
                {
                    command.Connection = connection;
                    foreach (object param in parameters)
                    {
                        command.Parameters.Add(new OleDbParameter("?", OleDbType.BSTR)).Value = param;
                    }
                    try
                    {
                        connection.Open();
                        return command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return null;
        }

        private DataSet SqlReaderQuery(string commandText, params object[] parameters)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(commandText))
                {
                    command.Connection = connection;
                    foreach (object param in parameters)
                    {
                        command.Parameters.Add(new OleDbParameter("?", OleDbType.BSTR)).Value = param;
                    }
                    try
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        connection.Open();
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;

                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return null;
        }

        private User GetUser(string username)
        {
            User user = null;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM [UserDatabase] WHERE [Username] = ?"))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new OleDbParameter("?", OleDbType.BSTR)).Value = username;

                    try
                    {
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("PlayerId"));
                            string name = reader.GetString(reader.GetOrdinal("Username"));
                            string passwordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                            user = new User(id, name, passwordHash);
                        }
                        else
                        {
                            LogHelper.ErrorLog("No user found");
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return user;
        }

        // User table methods
        private int GetUserId(string username)
        {
            User user = GetUser(username);
            if (user == null)
            {
                return -1;
            }
            return GetUser(username).PlayerId;
        }


        public bool UserExists(string Username)
        {
            bool exists = true;
            object count = SqlScalarQuery("SELECT COUNT (*) FROM [UserDatabase] WHERE [Username] = ?;", Username);
            if ((int)count == 0)
            {
                exists = false;
                LogHelper.Log(string.Format("Username : {0} does not exist", Username));
            }
            else
            {
                LogHelper.Log(string.Format("Username : {0} exists", Username));
            }
            return exists;
        }

        public bool AddUser(string Username, string Password)
        {
            if (UserExists(Username)) return false;

            // Get new id
            int id = (int)SqlScalarQuery("SELECT COUNT (PlayerId) FROM [UserDatabase];");
            Console.WriteLine(id);
            bool success = SqlNonQuery("INSERT INTO [UserDatabase] VALUES (?, ?, ?);", id, Username, CalculateHash(Password));
            if (success)
                LogHelper.Log(String.Format("Successfully added new user {0}", Username));
            return success;
        }


        public User Authenticate(string Username, string Password)
        {
            if (!UserExists(Username)) return null;
            User user = GetUser(Username);
            bool valid = user.PasswordHash == CalculateHash(Password);

            if (valid)
            {
                LogHelper.Log(string.Format("Authentication for {0} successful", Username));
                return user;
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Password hash for {0} doesn't match database, expected {1}", Username, user.PasswordHash));
            }
            return null;
        }

        private string CalculateHash(string s)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            var hashAlg = SHA256.Create();
            byte[] hash = hashAlg.ComputeHash(buffer);
            string hashedString = Convert.ToBase64String(hash);
            return hashedString;
        }

        // Score table methods

        private Game GetScore(int GameId)
        {
            Game game = null;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM [ScoreDatabase] WHERE [GameId] = ?"))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new OleDbParameter("?", OleDbType.BSTR)).Value = GameId;

                    try
                    {
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int playerId = reader.GetInt32(reader.GetOrdinal("PlayerId"));
                            int score = reader.GetInt32(reader.GetOrdinal("Score"));
                            string signature = reader.GetString(reader.GetOrdinal("Signature"));
                            game = new Game(GameId, playerId, score, signature);
                        }
                        else
                        {
                            LogHelper.ErrorLog("No game found");
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return game;
        }


        public void AddScore(User user, int score)
        {
            if (user == null) return;
            string newSqlInsert = "INSERT INTO ScoreDatabase (GameId, PlayerId, Score, Signature)" +
                "VALUES (?, ?, ?, ?)";
            int nextGameId = (int)SqlScalarQuery("SELECT COUNT (GameId) FROM [ScoreDatabase];");
            SqlNonQuery(newSqlInsert, new object[] { nextGameId, user.PlayerId, score, CalculateSignature(nextGameId, user.PlayerId, score) });
        }

        private string CalculateSignature(int gameId, int playerId, int score)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            string KEY = "zADPbCHNH";
            string data = gameId.ToString() + playerId.ToString() + score.ToString();
            string hash = CalculateHash(data).Replace("=", "");

            HillCipher cipher = new HillCipher(KEY, alphabet);
            string signature = cipher.Encrypt(hash);
            LogHelper.Log("hash is " + hash);
            LogHelper.Log("Signature is " + signature);
            LogHelper.Log("Decrypted signature is " + cipher.Decrypt(signature));
            return cipher.Encrypt(hash);
        }


        public bool VerifyGameScore(Game game)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            string KEY = "zADPbCHNH";
            string data = game.GameId.ToString() + game.PlayerId.ToString() + game.Score.ToString();
            string hash = CalculateHash(data).Replace("=", "");
            while (hash.Length % 3 != 0)
            {
                hash += alphabet[alphabet.Length - 1];
            }



            HillCipher cipher = new HillCipher(KEY, alphabet);
            string decryptedSignature = cipher.Decrypt(game.StoredSignature);

            if (decryptedSignature != hash)
            {
                LogHelper.Log(String.Format("Game id {0}, decrypted signature was {1}, calculated signature was {2}", game.GameId.ToString(), decryptedSignature, hash));
                return false;
            }
            else
            {
                LogHelper.Log(String.Format("Match for game id {0} decrypted signature was {1}, calculated signature was {2}", game.GameId.ToString(), decryptedSignature, hash));
                return true;
            }

        }


        // viewing scores

        public DataSet GetAllScores()
        {
            string query = "SELECT ScoreDatabase.GameId, UserDatabase.Username, ScoreDatabase.Score FROM UserDatabase, ScoreDatabase WHERE UserDatabase.PlayerId = ScoreDatabase.PlayerId";
            DataSet ds = SqlReaderQuery(query);

            ds.Tables[0].Columns.Add("Verified", typeof(bool));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row[3] = VerifyGameScore(GetScore((int)row[0]));
            }
            return ds;
        }

        public DataSet GetUserScores(User user)
        {
            string query = "SELECT ScoreDatabase.GameId, UserDatabase.Username, ScoreDatabase.Score FROM UserDatabase, ScoreDatabase WHERE UserDatabase.PlayerId = ScoreDatabase.PlayerId AND UserDatabase.PlayerId = ?";
            DataSet ds = SqlReaderQuery(query, new object[] { user.PlayerId });
            ds.Tables[0].Columns.Add("Verified", typeof(bool));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row[3] = VerifyGameScore(GetScore((int)row[0]));
            }
            return ds;
        }

    }

}
