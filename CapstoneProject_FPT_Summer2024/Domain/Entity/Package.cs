﻿using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Package
{
    public int PackageId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<YearPackage> YearPackages { get; set; } = new List<YearPackage>();
}
