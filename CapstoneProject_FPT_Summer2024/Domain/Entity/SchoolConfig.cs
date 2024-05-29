using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class SchoolConfig
{
    public int ConfigId { get; set; }

    public int SchoolId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public virtual HighSchool School { get; set; } = null!;
}
