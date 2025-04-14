namespace USAble_Burger_API.Models.DTO
{
    public class OrderDTO
    {
        public double OrderTotal { get; set; }

        public int OrderTaker { get; set; }

        public string OrderItems { get; set; } = null!;
    }
}
