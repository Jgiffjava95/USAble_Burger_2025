namespace USAble_Burger_API.Models.DTO
{
    public class OrderDTO
    {
        public double OrderTotal { get; set; }
        public double OrderSubTotal { get; set; }
        public double OrderDiscountAmount { get; set; }
        public double OrderPreTaxTotal { get; set; }
        public double OrderTaxAmount { get; set; }
        public int OrderTaker { get; set; }
        public List<Item> OrderItems { get; set; } = null!;
        public long OrderDate { get; set; }
    }
}
