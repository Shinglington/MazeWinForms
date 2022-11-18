using ADOX;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data.OleDb;
using System.IO;

namespace PRJ_MazeWinForms.Authentication
{
    public abstract class DatabaseHelper
    {
        protected string _databaseName;
        protected string _connectionString;

        protected void CreateDatabase()
        {
            CatalogClass cat = new CatalogClass();

            if (!File.Exists(_databaseName))
            {
                cat.Create(_connectionString);
                CreateTables();
                LogHelper.Log("Database created");
            }
            else
            {
                LogHelper.Log("Database already exists");
            }
            cat = null;
        }

        protected virtual void CreateTables()
        {

        }

        protected void ExecuteSql(string sqlString)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                OleDbCommand command = new OleDbCommand(sqlString);

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

        protected void SqlQuery(string sqlString)
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

    public class UserDatabase : DatabaseHelper
    {
        public UserDatabase()
        {
            _databaseName = "UserDatabase.mdf";
            _connectionString = @"Provider=Microsoft Jet 4.0 OLE DB Provider; Data Source = " + _databaseName + ";";
            CreateDatabase();
        }

        protected override void CreateTables()
        {
            string _creationString = "CREATE TABLE Users("
                + "UserId SHORT NOT NULL,"
                + "Username VARCHAR(13),"
                + "PasswordHash VARCHAR(20),"
                + "}";
            ExecuteSql(_creationString);
        }

        public
    }

}
