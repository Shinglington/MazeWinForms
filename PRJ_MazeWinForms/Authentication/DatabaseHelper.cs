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
        private string _connectionString;

        public DatabaseHelper()
        {
            string directory = Directory.GetParent(typeof(Program).Assembly.Location).FullName;
            _connectionString = @"Provider = Microsoft Jet 4.0 OLE DB Provider;Data Source = " + directory + @"\" + _databaseName + ";";
            CreateDatabase();
        }


        private void CreateDatabase()
        {

            if (!File.Exists(_databaseName))
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
            string _creationString =
                "CREATE TABLE [UserDatabase] ("
                + "[Id] INT NOT NULL,"
                + "[Username] VARCHAR(13) NOT NULL,"
                + "[Password] VARCHAR(13) NOT NULL,"
                + "PRIMARY KEY(Id)"
                + ");";

            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(_creationString))
                {
                    command.Connection = connection;

                    try
                    {
                        connection.Open();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
        }

        public void AddUser(string Username, string PassHash)
        {
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
                    command.Parameters.Add("?", OleDbType.VarChar).Value = PassHash;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
        }

        public bool Authenticate(string Username, string Password)
        {
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
            bool valid = passhash == Password;
            if (valid)
            {
                LogHelper.Log("Success");
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Password for {0} doesn't match database, expected {1}", Username, passhash));
            }
            return valid;
        }

        public bool Open()
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {

            }

            return true;
        }

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
                            Console.WriteLine(reader[0].ToString());
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
