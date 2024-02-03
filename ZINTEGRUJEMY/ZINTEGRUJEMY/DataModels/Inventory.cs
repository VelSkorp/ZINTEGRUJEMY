namespace ZINTEGRUJEMY
{
	public class Inventory
	{
		public int ProductID { get; set; }
		public string SKU { get; set; }
		public string Unit { get; set; }
		public int Quantity { get; set; }
		public string Manufacturer { get; set; }
		public int Shipping { get; set; }
		public decimal ShippingCost { get; set; }
	}
}