using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USAble_Burger_API.Models;
using USAble_Burger_API.Models.DTO;

namespace USAble_Burger_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class USAbleBurgerApi(UsableBurgerContext context) : ControllerBase
    {
        [HttpGet("/api/items", Name = "GetItemCollection")]
        public async Task<ActionResult<List<Item>>> GetItemCollection()
        {
            var foundItems = await context.Items.ToListAsync();
            if (foundItems == null) 
            {
                return NotFound();
            }
            return foundItems;
        }

        [HttpGet("/api/login", Name = "GetUserByUsername")]
        public async Task<ActionResult<UserDTO>> GetUserByUserName(string userName, string password)
        {
            var foundUser = await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            if (foundUser == null)
            {
                return NotFound("User account was not found.");
            } else if (foundUser.UserPassword == password)
            {
                return Ok(new UserDTO(foundUser.UserId, foundUser.UserName));
            }
         return BadRequest("Username or password may be incorrect.");   
        }

        [HttpGet("/api/orders", Name = "GetOrdersByUserId")]
        public async Task<ActionResult<List<Order>>> GetOrdersByUserId(int userId)
        {
            var foundOrders = await context.Orders.Where(o => o.OrderTaker == userId).ToListAsync();
            if (foundOrders == null)
            {
                return NotFound("No orders found for that user.");
            }
            return Ok(foundOrders);
        }

        [HttpGet("/api/discounts", Name = "GetDiscountCollection")]
        public async Task<ActionResult<List<Discount>>> GetDiscountCollection()
        {
            var foundDiscounts = await context.Discounts.ToListAsync();
            if (foundDiscounts == null)
            {
                return NotFound("No discounts found.");
            }
            return foundDiscounts;
        }

        [HttpPost("/api/create_order", Name = "NewOrder")]
        public async Task<ActionResult> NewOrder([FromBody] OrderDTO body)
        {
            if (body.OrderItems != null)
            {
                Order order = new Order();
                order.OrderTotal = body.OrderTotal;
                order.OrderTaker = body.OrderTaker;
                order.OrderItems = body.OrderItems;
                order.OrderId = 0;

                context.Orders.Add(order);
                await context.SaveChangesAsync();

                return Ok("Order created.");
            }
            return BadRequest("Order creation failed.");
        }
    }
}