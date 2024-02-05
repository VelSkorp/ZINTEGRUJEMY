using CsvHelper.Configuration;

namespace ZINTEGRUJEMY
{
	public class PriceCsvMap : ClassMap<Price>
	{
		public PriceCsvMap()
		{
			Map(m => m.ID).Index(0);
			Map(m => m.SKU).Index(1);
			Map(m => m.NettPrice).Index(2);
			Map(m => m.NettPriceAfterDiscount).Index(3);
			Map(m => m.VatRate).Index(4).TypeConverter<CustomDoubleConverter>();
			Map(m => m.NettPriceAfterDiscountForLogisticUnit).Index(5);
		}
	}
}