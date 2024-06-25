using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class HighSchool
{
    public int SchoolId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? ImageUrl { get; set; }

    public string? WebUrl { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Penalty> Penalties { get; set; } = new List<Penalty>();

    public virtual ICollection<RegisteredSchool> RegisteredSchools { get; set; } = new List<RegisteredSchool>();

    public virtual SchoolAdmin? SchoolAdmin { get; set; }

    public virtual ICollection<SchoolConfig> SchoolConfigs { get; set; } = new List<SchoolConfig>();

    public virtual ICollection<SchoolYear> SchoolYears { get; set; } = new List<SchoolYear>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
