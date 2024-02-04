namespace ZINTEGRUJEMY
{
	public class Price
	{
		public string ID { get; set; }
		public string SKU { get; set; }
		public double NettPrice { get; set; }
		public double NettPriceAfterDiscount { get; set; }
		public double VatRate { get; set; }
		public double? NettPriceAfterDiscountForLogisticUnit { get; set; }
	}
}