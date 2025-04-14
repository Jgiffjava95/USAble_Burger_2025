using System;
using System.Collections.Generic;

namespace USAble_Burger_API.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string DiscountName { get; set; } = null!;

    public double DiscountPercentage { get; set; }
}
