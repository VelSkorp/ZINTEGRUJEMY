namespace ZINTEGRUJEMY
{
	public class Price
	{
		public string ID { get; set; }
		public string SKU { get; set; }
		public decimal NettPrice { get; set; }
		public decimal NettPriceAfterDiscount { get; set; }
		public decimal VatRate { get; set; }
		public decimal NettPriceAfterDiscountForLogisticUnit { get; set; }
	}
}