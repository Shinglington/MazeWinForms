using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using ADOX;
using PRJ_MazeWinForms.Logging;

namespace PRJ_MazeWinForms.Authentication
{
    public abstract class DatabaseHelper
    {
        protected string _databaseName;
        protected string creationString;
        protected string connectionString;

        protected void CreateDatabase()
        {
            CatalogClass cat = new CatalogClass();

            if (!File.Exists(_databaseName))
            {
                cat.Create(connectionString);
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
            using (OleDbConnection connection = new OleDbConnection(connectionString))
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


        public bool Open()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
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
            connectionString = @"Provider=Microsoft Jet 4.0 OLE DB Provider; Data Source = " + _databaseName + ";";
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
    }

}
