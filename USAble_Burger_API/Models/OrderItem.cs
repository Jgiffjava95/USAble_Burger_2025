using System;
using System.Collections.Generic;

namespace USAble_Burger_API.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public string OrderItemName { get; set; } = null!;

    public int OrderItemQuan { get; set; }

    public int ItemId { get; set; }

    public int OrderId { get; set; }

}
