using Microsoft.AspNetCore.Mvc;

namespace ZINTEGRUJEMY
{
	[ApiController]
	[Route("ZINTEGRUJEMY.pl/api/products")]
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

		[HttpPost("process-csv")]
		public async Task<IActionResult> ProcessCsvAsync([FromBody] string productsLink)
		{
			//test
			var fileName = await _csvDownloader.DownloadAndSaveCsvAsync(productsLink);
			var allProducts = _csvReader.ReadCsv<Product>(fileName);
			var filtered = allProducts.Where(product => !product.IsWire && product.Shipping.Equals("24h"));
			_sqlWriter.WriteToTable(filtered, "Products");

			return Ok("Ok");
		}

		[HttpGet("product-info/{sku}")]
		public IActionResult GetProductInfo(string sku)
		{
			return Ok("Ok");
		}
	}
}