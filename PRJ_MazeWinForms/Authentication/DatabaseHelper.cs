using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace PRJ_MazeWinForms.Authentication
{
    public class DatabaseHelper
    {
        private const string _databaseName = "MazeDatabase.mdf";
        private const string _connectionString = @"Provider = Microsoft Jet 4.0 OLE DB Provider;Data Source = " + _databaseName + ";";

        public DatabaseHelper()
        {
            CreateDatabase();
        }


        private void CreateDatabase()
        {
            CatalogClass cat = new CatalogClass();
            if (!File.Exists(_databaseName))
            {
                cat.Create(_connectionString);
                LogHelper.Log("Created Database successfully");
                CreateTables();
            }
            else
            {
                LogHelper.ErrorLog("Database already exists");
            }
            cat = null;

        }

        private void CreateTables()
        {
            string _creationString =
                "CREATE TABLE Users ("
                + "UserId SHORT NOT NULL,"
                + "Username VARCHAR(13) NOT NULL,"
                + "Password VARCHAR(13) NOT NULL,"
                + "PRIMARY KEY(UserId)"
                + ");";

            ExecuteSql(_creationString, new string[0] { });
        }

        private int ExecuteSql(string sqlString, string[] parameters)
        {
            int linesAffected = -1;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(sqlString))
                {
                    command.Connection = connection;
                    foreach (string s in parameters)
                    {
                        command.Parameters.Add(new OleDbParameter("@Parameter", s));
                    }

                    try
                    {
                        connection.Open();
                        linesAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
            return linesAffected;
            
        }

        private void SqlQuery(string sqlString)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                OleDbCommand command = new OleDbCommand(sqlString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
        }

        public void AddUser(string Username, string PassHash)
        {
            int id = 0;
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT COUNT (UserId) FROM Users;", connection))
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
            string sqlString = string.Format("INSERT INTO Users " +
                "VALUES (?, '?', '?');");
            ExecuteSql(sqlString, new string[3] {id.ToString(), Username, PassHash});
        }

        public bool Authenticate(string Username, string Password)
        {
            string passhash = "";
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand("SELECT Password FROM Users WHERE Username = ?", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Username", Username);
                    passhash = (string) command.ExecuteScalar();
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


    }

}
