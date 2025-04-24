using System;
using System.Collections.Generic;

namespace USAble_Burger_API.Models;

public partial class TaxType
{
    public int TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public double TaxPercentage { get; set; }
}
