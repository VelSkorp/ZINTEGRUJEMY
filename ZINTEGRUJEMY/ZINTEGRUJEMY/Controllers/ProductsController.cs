using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ZINTEGRUJEMY.DataModels;

namespace ZINTEGRUJEMY
{
	[ApiController]
	[Route("ZINTEGRUJEMY.pl/api")]
	public class ProductsController : ControllerBase
	{
		private readonly CsvDownloader _csvDownloader;
		private readonly CsvReader _csvReader;
		private readonly SqlWriter _sqlWriter;

		public ProductsController(CsvDownloader csvDownloader, CsvReader csvReader, SqlWriter sqlWriter)
		{
			_csvDownloader = csvDownloader;
			_csvReader = csvReader;
			_sqlWriter = sqlWriter;
		}

		[HttpPost("products")]
		public async Task<IActionResult> ProcessCsvAsync([FromBody] LinksToSource links)
		{
			try
			{
                var fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Products);
                var allProducts = _csvReader.ReadCsv<Product>(fileName);
                var filteredProducts = allProducts.Where(product => !product.IsWire && product.Shipping.Equals("24h"));
                _sqlWriter.WriteToTable(filteredProducts, "Products");

                fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Inventory);
                var allInventories = _csvReader.ReadCsv<Inventory>(fileName);
                var filteredInventories = allInventories.Where(inventory => inventory.Shipping.Equals("24h"));
                _sqlWriter.WriteToTable(filteredInventories, "Inventory");

                fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Prices);
				var prices = _csvReader.ReadCsv<Price>(fileName);
				_sqlWriter.WriteToTable(prices, "Prices");
			}
			catch (FileNotFoundException ex)
			{
				Log.Fatal(ex.Message);
				return Problem();
			}
			catch (TypeConverterException ex)
			{
				Log.Fatal($"Fatal error in the field {ex.Text} when converting a record: {ex.Message}");
				return Problem();
			}
			catch (BadDataException ex)
			{
				Log.Fatal($"Fatal error in the field {ex.Field} when reading a record: {ex.RawRecord}");
				return Problem();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex.Message);
				return Problem();
			}

			return Accepted("Data successfully downloaded and saved");
		}

		[HttpGet("product-info/{sku}")]
		public IActionResult GetProductInfo(string sku)
		{
			return Ok("Ok");
		}
	}
}