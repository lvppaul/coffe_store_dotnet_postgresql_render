using System;
using System.Collections.Generic;

namespace PRN232.Lab1.CoffeeStore.Repositories.Models;

public partial class ProductInMenu
{
    public int ProductInMenuId { get; set; }

    public int ProductId { get; set; }

    public int MenuId { get; set; }

    public int Quantity { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
