namespace USAble_Burger_API.Models;

public class UserDTO
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public UserDTO(int UserId, string UserName) {
        this.UserId = UserId;
        this.UserName = UserName;
    }

}
