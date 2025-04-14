using System;
using System.Collections.Generic;
using USAble_Burger_API.Models;

namespace USAble_Burger_API;

public partial class Order
{
    public int OrderId { get; set; }

    public double OrderTotal { get; set; }

    public int OrderTaker { get; set; }

    public string OrderItems { get; set; } = null!;
}
