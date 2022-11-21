using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PRJ_MazeWinForms.Authentication
{
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

        private object SqlReaderQuery(string commandText, params object[] parameters)
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
                        return command.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return null;
        }

        // User table methods
        private object GetUser(string Username)
        {
            object user = SqlReaderQuery("SELECT * FROM [UserDatabase] WHERE [Username] = ?;", Username);
            return user;
        }
        private bool UserExists(string Username)
        {
            bool exists = false;
            object user = SqlScalarQuery("SELECT [PlayerId] FROM [UserDatabase] WHERE [Username] = ?;", Username);
            if (user != null)
            {
                exists = true;
                LogHelper.Log(string.Format("Username : {0} exists", Username));
            }
            else
            {
                LogHelper.Log(string.Format("Username : {0} does not exist", Username));
            }
            return exists;
        }

        public bool AddUser(string Username, string Password)
        {
            if (UserExists(Username)) return false;

            // Get new id
            int id = 0;
            object queryResponse = SqlScalarQuery("SELECT COUNT (PlayerId) FROM [UserDatabase];");
            if (queryResponse != null)
            {
                id = (int)queryResponse;
            }

            bool success =  SqlNonQuery("INSERT INTO [UserDatabase VALUES (?, ?, ?);", id, Username, CalculateHash(Password));
            if (success)
                LogHelper.Log(String.Format("Successfully added new user {0}", Username));
            return success;
        }


        public bool Authenticate(string Username, string Password)
        {
            if (!UserExists(Username)) return false;

            string storedHash = (string)SqlScalarQuery("SELECT[PasswordHash] FROM UserDatabase WHERE [Username] = ?; ", Username);
            bool valid = storedHash == CalculateHash(Password);

            if (valid)
            {
                LogHelper.Log(string.Format("Authentication for {0} successful", Username));
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Password hash for {0} doesn't match database, expected {1}", Username, storedHash));
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
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT [Username], [Password] FROM UserDatabase;", connection))
                {
                    connection.Open();
                    try
                    {
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }

                }
            }
        }
    }

}
