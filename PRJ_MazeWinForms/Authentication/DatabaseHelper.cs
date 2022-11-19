using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

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
                LogHelper.Log("Created Database successfully");
                CreateTables();
            }
            else
            {
                LogHelper.ErrorLog("Database already exists");
            }
        }
        private void CreateTables()
        {
            // User table
            string userTableCreation =
                "CREATE TABLE [UserDatabase] ("
                + "[PlayerId] INT NOT NULL,"
                + "[Username] VARCHAR(13) NOT NULL,"
                + "[Password] VARCHAR(13) NOT NULL,"
                + "PRIMARY KEY(Id)"
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

            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(userTableCreation))
                {
                    command.Connection = connection;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        LogHelper.Log("User Table created");
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
                using (OleDbCommand command = new OleDbCommand(scoreTableCreation))
                {
                    command.Connection = connection;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        LogHelper.Log("Score Table created");
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
        }


        // User table methods

        public bool AddUser(string Username, string Password)
        {
            if (UserExists(Username)) return false;

            // Get new id
            int id = 0;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT COUNT (Id) FROM [UserDatabase];", connection))
                {
                    connection.Open();
                    try
                    {
                        id = (int)command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                        return false;
                    }
                }
            }


            string sqlString = string.Format("INSERT INTO [UserDatabase] " +
                "VALUES (?, ?, ?);");

            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(sqlString))
                {
                    command.Connection = connection;
                    command.Parameters.Add("?", OleDbType.Integer).Value = id;
                    command.Parameters.Add("?", OleDbType.VarChar).Value = Username;
                    command.Parameters.Add("?", OleDbType.VarChar).Value = CalculateHash(Password);
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                        return false;
                    }
                }
            }
            return true;
        }

        private bool UserExists(string Username)
        {
            bool exists = false;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT [Id] FROM [UserDatabase] WHERE [Username] = ?;"))
                {
                    command.Connection = connection;
                    command.Parameters.Add("?", OleDbType.VarChar).Value = Username;
                    connection.Open();
                    try
                    {
                        if (command.ExecuteScalar() != null)
                        {
                            exists = true;
                            LogHelper.Log(string.Format("Username : {0} exists", Username));
                        }
                        else
                        {
                            LogHelper.Log(string.Format("Username : {0} does not exist", Username));
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return exists;
        }

        public bool Authenticate(string Username, string Password)
        {
            if (!UserExists(Username))
            {
                return false;
            }

            string passhash = "";
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT [Password] FROM UserDatabase WHERE[Username] = ?;", connection))
                {
                    connection.Open();
                    command.Parameters.Add("?", OleDbType.VarChar).Value = Username;
                    try
                    {
                        passhash = (string)command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }

                }
            }
            bool valid = passhash == CalculateHash(Password);
            if (valid)
            {
                LogHelper.Log(string.Format("Authentication for {0} successful", Username));
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Password for {0} doesn't match database, expected {1}", Username, passhash));
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
