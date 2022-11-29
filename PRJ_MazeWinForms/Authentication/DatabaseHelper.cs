using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

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
                + "[PasswordHash] VARCHAR(13) NOT NULL,"
                + "PRIMARY KEY(PlayerId)"
                + ");";

            // Score table
            string scoreTableCreation =
                "CREATE TABLE [ScoreDatabase] ("
                + "[GameId] INT NOT NULL,"
                + "[PlayerId] INT NOT NULL,"
                + "[Score] INT NOT NULL,"
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
                            LogHelper.ErrorLog("No logs found");
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
            return s;
        }

        // Score table methods

        public void AddScore(User user, int score)
        {
            if (user == null) return;
            string newSqlInsert = "INSERT INTO ScoreDatabase (GameId, PlayerId, Score)" +
                "VALUES (?, ?, ?)";
            int nextGameId = (int)SqlScalarQuery("SELECT COUNT (GameId) FROM [ScoreDatabase];");
            SqlNonQuery(newSqlInsert, new object[] { nextGameId, user.PlayerId, score });
        }



        // viewing scores

        public DataSet GetAllScores()
        {
            string query = "SELECT UserDatabase.Username, ScoreDatabase.Score FROM UserDatabase, ScoreDatabase WHERE UserDatabase.PlayerId = ScoreDatabase.PlayerId";
            DataSet ds = SqlReaderQuery(query);
            return ds;
        }

        public DataSet GetUserScores(User user)
        {
            string query = "SELECT UserDatabase.Username, ScoreDatabase.Score FROM UserDatabase, ScoreDatabase WHERE UserDatabase.PlayerId = ScoreDatabase.PlayerId AND UserDatabase.PlayerId = ?";
            DataSet ds = SqlReaderQuery(query, new object[] {user.PlayerId});
            return ds;
        }

    }

}
