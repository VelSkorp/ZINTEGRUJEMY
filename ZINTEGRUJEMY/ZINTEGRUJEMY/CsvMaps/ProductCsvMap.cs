using CsvHelper.Configuration;

namespace ZINTEGRUJEMY
{
	public class ProductCsvMap : ClassMap<Product>
	{
		public ProductCsvMap()
		{
			Map(m => m.ID).Index(0);
			Map(m => m.SKU).Index(1);
			Map(m => m.Name).Index(2);
			Map(m => m.EAN).Index(4);
			Map(m => m.ProducerName).Index(6);
			Map(m => m.Category).Index(7);
			Map(m => m.IsWire).Index(8);
			Map(m => m.Shipping).Index(9);
			Map(m => m.Available).Index(11);
			Map(m => m.IsVendor).Index(16);
			Map(m => m.DefaultImage).Index(18);
		}
	}
}