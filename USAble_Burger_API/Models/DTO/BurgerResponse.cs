namespace USAble_Burger_API.Models.DTO
{
    public class BurgerResponse
    {
        public object data { get; set; }

        public string Message { get; set; } = null!;

        public bool Success { get; set; } = false;

        public BurgerResponse(object data, string message, bool success)
        {
            this.data = data;
            this.Message = message;
            this.Success = success;
        }
    }
}
