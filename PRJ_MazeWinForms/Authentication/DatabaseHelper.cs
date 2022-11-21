using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;

namespace PRJ_MazeWinForms.Authentication
{
    
    class User 
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
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return null;
        }

        private DataTable SqlReaderQuery(string commandText, params object[] parameters)
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
                        OleDbDataReader reader = command.ExecuteReader();
                        return reader.GetSchemaTable();
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
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM [UserDatabase] WHERE [Username] = ?)"))
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
        private int GetUserId(string Username)
        {
            User user = GetUser(Username);
            if (user == null)
            {
                return -1;
            }
            return GetUser(Username).PlayerId;
        }

        
        private bool UserExists(string Username)
        {
            bool exists = true;
            object count = SqlScalarQuery("SELECT COUNT (*) FROM [UserDatabase] WHERE [Username] = ?;", Username);
            if ((int) count == 0)
            {
                LogHelper.Log(string.Format("Username : {0} does not exist", Username));
            }
            else
            {
                exists = false;
                LogHelper.Log(string.Format("Username : {0} exists", Username));
            }
            return exists;
        }

        public bool AddUser(string Username, string Password)
        {
            if (UserExists(Username)) return false;

            // Get new id
            int id = (int) SqlScalarQuery("SELECT COUNT (PlayerId) FROM [UserDatabase];");
            Console.WriteLine(id);
            bool success = SqlNonQuery("INSERT INTO [UserDatabase] VALUES (?, ?, ?);", id, Username, CalculateHash(Password));
            if (success)
                LogHelper.Log(String.Format("Successfully added new user {0}", Username));
            return success;
        }


        public bool Authenticate(string Username, string Password)
        {
            if (!UserExists(Username)) return false;
            User user = GetUser(Username);
            bool valid = user.PasswordHash == CalculateHash(Password);

            if (valid)
            {
                LogHelper.Log(string.Format("Authentication for {0} successful", Username));
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Password hash for {0} doesn't match database, expected {1}", Username, user.PasswordHash));
            }
            return valid;
        }

        private string CalculateHash(string s)
        {
            return s;
        }

        // Score table methods

















        public void ShowDatabase()
        {
            DataTable table = SqlReaderQuery("SELECT * FROM [UserDatabase]");
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("{0,-14}", col.ColumnName);
                }
                Console.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        if (col.DataType.Equals(typeof(DateTime)))
                            Console.Write("{0,-14:d}", row[col]);
                        else if (col.DataType.Equals(typeof(Decimal)))
                            Console.Write("{0,-14:C}", row[col]);
                        else
                            Console.Write("{0,-14}", row[col]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

    }

}
