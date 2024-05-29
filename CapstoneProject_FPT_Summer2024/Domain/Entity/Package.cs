using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Package
{
    public int PackageId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public int? Price { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<YearPackage> YearPackages { get; set; } = new List<YearPackage>();
}
