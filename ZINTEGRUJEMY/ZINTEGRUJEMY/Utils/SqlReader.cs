using Dapper;
using Microsoft.Data.Sqlite;

namespace ZINTEGRUJEMY
{
	public class SqlReader
	{
		private readonly string _connectionString;

		public SqlReader(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<ProductInfo> GetProductDetailsBySKUAsync(string sku)
		{
			using (var dbConnection = new SqliteConnection(_connectionString))
			{
				await dbConnection.OpenAsync();

				var query = @"
					SELECT
						P.Name,
						P.EAN,
						P.ProducerName,
						P.Category,
						P.DefaultImage,
						I.Quantity,
						I.Unit,
						PR.NettPrice,
						PR.NettPriceAfterDiscountForLogisticUnit
					FROM Products P
					LEFT JOIN Inventory I ON P.ID = I.ProductID
					LEFT JOIN Prices PR ON P.SKU = PR.SKU
					WHERE P.SKU = @SKU";

				return await dbConnection.QuerySingleOrDefaultAsync<ProductInfo>(query, new { SKU = sku });
			}
		}
	}
}