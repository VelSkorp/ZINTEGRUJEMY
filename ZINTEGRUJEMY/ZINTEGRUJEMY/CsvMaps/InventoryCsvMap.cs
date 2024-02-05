using CsvHelper.Configuration;

namespace ZINTEGRUJEMY
{
	public class InventoryCsvMap : ClassMap<Inventory>
	{
		public InventoryCsvMap()
		{
			Map(m => m.ProductID).Index(0);
			Map(m => m.SKU).Index(1);
			Map(m => m.Unit).Index(2);
			Map(m => m.Quantity).Index(3);
			Map(m => m.Manufacturer).Index(4);
			Map(m => m.Shipping).Index(6);
			Map(m => m.ShippingCost).Index(7);
		}
	}
}