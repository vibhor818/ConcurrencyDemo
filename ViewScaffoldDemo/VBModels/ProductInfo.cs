using System;
using System.Collections.Generic;

namespace ViewScaffoldDemo.VBModels;

public partial class ProductInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }
}
