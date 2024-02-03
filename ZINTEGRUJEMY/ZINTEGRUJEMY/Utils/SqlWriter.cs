using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ZINTEGRUJEMY
{
    public class SqlWriter
    {
        private const string FilePath = "Data/ProductsDataBaseSchema.sql";
        private readonly string _connectionString;

        public SqlWriter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void WriteToTable<T>(IEnumerable<T> data, string tableName)
        {
            using (var dbConnection = new SqliteConnection(_connectionString))
            {
                dbConnection.Open();

                CreateTablesIfNotExists<T>(dbConnection);

                dbConnection.Execute($"INSERT INTO {tableName} VALUES (@prop1, @prop2, @prop3, ...)", data);
            }
        }

        private void CreateTablesIfNotExists<T>(IDbConnection dbConnection)
        {
            try
            {
                string sqlScript;
                using (var sr = new StreamReader(FilePath))
                {
                    sqlScript = sr.ReadToEnd();
                }

                dbConnection.Execute(sqlScript);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when loading a database schema from a file: {ex.Message}");
            }
        }
    } 
}