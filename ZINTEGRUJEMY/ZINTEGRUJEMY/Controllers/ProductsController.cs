using Microsoft.AspNetCore.Mvc;

namespace ZINTEGRUJEMY
{
	[ApiController]
	[Route("ZINTEGRUJEMY.pl/api/products")]
	public class ProductsController : ControllerBase
	{
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