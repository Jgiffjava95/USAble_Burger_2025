namespace USAble_Burger_API.Models;

public class UserDTO
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public UserDTO(int userId, string userName, string firstName, string lastName) {
        this.UserId = userId;
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

}
