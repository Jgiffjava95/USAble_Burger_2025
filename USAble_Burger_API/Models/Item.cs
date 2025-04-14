using System;
using System.Collections.Generic;

namespace USAble_Burger_API.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public double ItemPrice { get; set; }
}
