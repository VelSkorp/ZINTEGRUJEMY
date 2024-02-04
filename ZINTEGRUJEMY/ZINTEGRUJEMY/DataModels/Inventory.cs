namespace ZINTEGRUJEMY
{
	public class Inventory
	{
		public int ProductID { get; set; }
		public string SKU { get; set; }
		public string Unit { get; set; }
		public double Quantity { get; set; }
		public string Manufacturer { get; set; }
		public string Shipping { get; set; }
		public double? ShippingCost { get; set; }
	}
}