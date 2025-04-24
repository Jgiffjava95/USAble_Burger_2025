using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USAble_Burger_API.DatabaseConnection;
using USAble_Burger_API.Models;
using USAble_Burger_API.Models.DTO;

namespace USAble_Burger_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class USAbleBurgerApi(UsableBurgerContext context) : ControllerBase
    {
        [HttpGet("/api/items", Name = "GetItemCollection")]
        public async Task<ActionResult<BurgerResponse>> GetItemCollection()
        {
            var foundItems = await context.Items.ToListAsync();
            if (foundItems == null)
            {
                return Ok(new BurgerResponse(new List<Item>(), "No items found.", false));
            }
            return Ok(new BurgerResponse(foundItems, "Items found.", true));
        }

        [HttpGet("/api/items_by_order", Name = "GetItemsByOrder")]
        public async Task<ActionResult<BurgerResponse>> GetItemsByOrder([FromQuery] int orderId)
        {
            if (orderId > 0)
            {
                var foundItems = await context.OrderItems.Where(u => u.OrderId == orderId).ToListAsync();
                if (foundItems == null)
                {
                    return Ok(new BurgerResponse(new List<OrderItem>(), "No order items found for this order.", false));
                }
                return Ok(new BurgerResponse(foundItems, "Order items found.", true));
            }
            return Ok(new BurgerResponse(new List<OrderItem>(), "Improper order id.", false));
        }

        [HttpGet("/api/itemTypes", Name = "GetItemTypeCollection")]
        public async Task<ActionResult<BurgerResponse>> GetItemTypeCollection()
        {
            var foundItemTypes = await context.ItemTypes.ToListAsync();
            if (foundItemTypes == null)
            {
                return Ok(new BurgerResponse(new List<ItemType>(), "No item types found.", false));
            }
            return Ok(new BurgerResponse(foundItemTypes, "Item types found.", true));
        }

        [HttpGet("/api/login", Name = "GetUserByUsername")]
        public async Task<ActionResult<BurgerResponse>> GetUserByUserName([FromQuery] String userName, [FromQuery] String userPassword)
        {

            if ((userName != null && userPassword != null) || (userName != "" & userPassword != ""))
            {
                var foundUser = await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
                if (foundUser == null || foundUser.UserPassword != userPassword)
                {
                    return Ok(new BurgerResponse(new UserDTO(0, "", "", ""), "Username or password may be incorrect.", false));
                }
                else
                {                   
                    return Ok(new BurgerResponse(new UserDTO(foundUser.UserId, foundUser.UserName, foundUser.FirstName, foundUser.LastName), "Welcome, " + foundUser.UserName + "!", true));
                }
            }
            return Ok(new BurgerResponse(new UserDTO(0, "", "", ""), "Username or password cannot be blank", false));
        }

        [HttpGet("/api/orders", Name = "GetOrdersByUserId")]
        public async Task<ActionResult<BurgerResponse>> GetOrdersByUserId([FromQuery] int userId)
        {
            var foundOrders = await context.Orders.Where(o => o.OrderTaker == userId).ToListAsync();
            if (foundOrders == null)
            {
                return Ok(new BurgerResponse(new List<Order>(), "No orders found for that user.", false));
            }
            return Ok(new BurgerResponse(foundOrders, "Orders found for user.", true));
        }

        [HttpGet("/api/discounts", Name = "GetDiscountCollection")]
        public async Task<ActionResult<BurgerResponse>> GetDiscountCollection()
        {
            var foundDiscounts = await context.Discounts.ToListAsync();
            if (foundDiscounts == null)
            {
                return Ok(new BurgerResponse(new List<Discount>(), "No discounts found.", false));
            }
            return Ok(new BurgerResponse(foundDiscounts, "Discounts Found.", true));
        }

        [HttpGet("/api/tax_types", Name = "GetTaxTypesCollection")]
        public async Task<ActionResult<BurgerResponse>> GetTaxTypesCollection()
        {
            var foundTaxTypes = await context.TaxTypes.ToListAsync();
            if (foundTaxTypes == null)
            {
                return Ok(new BurgerResponse(new List<TaxType>(), "No tax types found.", false));
            }
            return Ok(new BurgerResponse(foundTaxTypes, "Tax types found.", true));
        }

        [HttpPost("/api/create_order", Name = "NewOrder")]
        public async Task<ActionResult<BurgerResponse>> NewOrder([FromBody] OrderDTO order)
        {
            // Normally I would have a user token I would use to verifty the user is logged in before allowing orders to go through
            if (order == null)
            {
                return Ok(new BurgerResponse("", "Order and order details cannot be null", false));
            }
            else if (order.OrderTaker < 0)
            {
                return Ok(new BurgerResponse("", "Order taker id cannot be negative", false));
            }
            else if (order.OrderTotal < 0 || order.OrderSubTotal < 0 || order.OrderDiscountAmount < 0 || order.OrderPreTaxTotal < 0 || order.OrderTaxAmount < 0)
            {
                return Ok(new BurgerResponse("", "Order currency values cannot be negative", false));
            }
            else if (order.OrderDate < 0)
            {
                return Ok(new BurgerResponse("", "Order date cannot be negative", false));
            }
            else if (order.OrderItems.Count < 0)
            {
                return Ok(new BurgerResponse("", "Order must contain order items", false));
            }

            Order newOrder = new Order();
            newOrder.OrderTotal = order.OrderTotal;
            newOrder.OrderDiscountAmount = order.OrderDiscountAmount;
            newOrder.OrderTaxAmount = order.OrderDiscountAmount;
            newOrder.OrderPreTaxTotal = order.OrderPreTaxTotal;
            newOrder.OrderSubTotal = order.OrderSubTotal;

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(order.OrderDate);
            newOrder.OrderDate = DateOnly.FromDateTime(dateTimeOffset.Date);

            newOrder.OrderTaker = order.OrderTaker;
            newOrder.OrderId = 0;

            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();

            context.Entry(newOrder).Reload();
            int orderId = newOrder.OrderId;
            List<string> checkedOrderItemNames = new List<string>();

            foreach (var item in order.OrderItems)
            {
                if (!checkedOrderItemNames.Contains(item.ItemName))
                {
                    int quan = 0;
                    OrderItem newOrderItem = new OrderItem();
                    foreach (var item2 in order.OrderItems)
                    {
                        if (item.ItemId == item2.ItemId)
                        {
                            quan++;
                        }
                    }

                    checkedOrderItemNames.Add(item.ItemName);

                    newOrderItem.OrderItemQuan = quan;
                    newOrderItem.OrderItemName = item.ItemName;
                    newOrderItem.ItemId = item.ItemId;
                    newOrderItem.OrderId = orderId;

                    context.OrderItems.Add(newOrderItem);
                    await context.SaveChangesAsync();
                }
            }
            return Ok(new BurgerResponse("", "Order was successfully created.", true));
        }
    }
}