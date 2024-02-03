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
		public IActionResult ProcessCsv()
		{
			return Ok("Ok");
		}

		[HttpGet("product-info/{sku}")]
		public IActionResult GetProductInfo(string sku)
		{
			return Ok("Ok");
		}
	}
}