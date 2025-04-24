using System;
using System.Collections.Generic;

namespace USAble_Burger_API.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public double OrderSubTotal { get; set; }

    public double OrderDiscountAmount { get; set; }

    public double OrderPreTaxTotal { get; set; }

    public double OrderTaxAmount { get; set; }

    public double OrderTotal { get; set; }

    public DateOnly OrderDate { get; set; }

    public int OrderTaker { get; set; }
}
