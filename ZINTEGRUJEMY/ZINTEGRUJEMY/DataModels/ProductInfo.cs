namespace ZINTEGRUJEMY
{
	public class ProductInfo
	{
		public string Name { get; set; }
		public string EAN { get; set; }
		public string ProducerName { get; set; }
		public string Category { get; set; }
		public string DefaultImage { get; set; }
		public double Quantity { get; set; }
		public string Unit { get; set; }
		public double NettPrice { get; set; }
		public double? NettPriceAfterDiscountForLogisticUnit { get; set; }
	}
}