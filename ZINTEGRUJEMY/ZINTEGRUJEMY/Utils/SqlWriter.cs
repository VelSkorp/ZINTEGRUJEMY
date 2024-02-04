using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using Serilog;

namespace ZINTEGRUJEMY
{
	public class SqlWriter
	{
		private const string FilePath = "Data/ProductsDataBaseSchema.sql";
		private readonly string _connectionString;
		private readonly Dictionary<string, string> _tablesValues = new Dictionary<string, string>()
		{
			{ "Products", "@ID, @SKU, @Name, @EAN, @ProducerName, @Category, @IsWire, @Available, @IsVendor, @DefaultImage" },
			{ "Inventory", "@ProductID, @SKU, @Unit, @Quantity, @Manufacturer, @Shipping, @ShippingCost" },
			{ "Prices", "@ID, @SKU, @NettPrice, @NettPriceAfterDiscount, @VatRate, @NettPriceAfterDiscountForLogisticUnit" },
		};

		public SqlWriter(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void WriteToTable<T>(IEnumerable<T> data, string tableName)
		{
			using (var dbConnection = new SqliteConnection(_connectionString))
			{
				dbConnection.Open();

				CreateTablesIfNotExists(dbConnection);

				dbConnection.Execute($"INSERT INTO {tableName} VALUES ({_tablesValues[tableName]})", data);
			}
		}

		private void CreateTablesIfNotExists(IDbConnection dbConnection)
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
				Log.Error($"Error when loading a database schema from a file: {ex.Message}");
				throw;
			}
		}
	} 
}