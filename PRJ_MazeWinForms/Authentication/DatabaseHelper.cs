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
                + "PassHash VARCHAR(13) NOT NULL,"
                + "PRIMARY KEY(UserId)"
                + ");";

            ExecuteSql(_creationString);
        }

        private void ExecuteSql(string sqlString)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(sqlString)) 
                {
                    command.Connection = connection;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog(e.ToString());
                    }
                }
            }
        }

        private void SqlQuery(string sqlString)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                OleDbCommand command = new OleDbCommand(sqlString, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                reader.Close();
            }
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
