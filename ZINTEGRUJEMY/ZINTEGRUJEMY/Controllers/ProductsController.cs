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
		private readonly SqlReader _sqlReader;

		public ProductsController(CsvDownloader csvDownloader, CsvReader csvReader, SqlWriter sqlWriter, SqlReader sqlReader)
		{
			_csvDownloader = csvDownloader;
			_csvReader = csvReader;
			_sqlWriter = sqlWriter;
			_sqlReader = sqlReader;
		}

		[HttpPost("products")]
		public async Task<IActionResult> ProcessCsvAsync([FromBody] LinksToSource links)
		{
			try
			{
				var fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Products);
				var allProducts = await _csvReader.ReadCsvAsync<Product>(fileName);
				var filteredProducts = allProducts.Where(product => !product.IsWire && product.Shipping.Equals("24h"));
				await _sqlWriter.WriteToTableAsync(filteredProducts, "Products");

				fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Inventory);
				var allInventories = await _csvReader.ReadCsvAsync<Inventory>(fileName);
				var filteredInventories = allInventories.Where(inventory => inventory.Shipping.Equals("24h"));
				await _sqlWriter.WriteToTableAsync(filteredInventories, "Inventory");

				fileName = await _csvDownloader.DownloadAndSaveCsvAsync(links.Prices);
				var prices = await _csvReader.ReadCsvAsync<Price>(fileName);
				await _sqlWriter.WriteToTableAsync(prices, "Prices");

				return Accepted("Data successfully downloaded and saved");
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
		}

		[HttpGet("product-info/{sku}")]
		public async Task<IActionResult> GetProductInfoAsync(string sku)
		{
			try
			{
				var productDetails = await _sqlReader.GetProductDetailsBySKUAsync(sku);

				if (productDetails == null)
				{
					return NotFound($"Product with SKU {sku} not found.");
				}

				return Ok(productDetails);
			}
			catch (Exception ex)
			{
				Log.Fatal(ex.Message);
				return Problem();
			}
		}
	}
}