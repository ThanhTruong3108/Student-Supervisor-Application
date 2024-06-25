using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class SchoolAdmin
{
    public int SchoolAdminId { get; set; }

    public int? SchoolId { get; set; }

    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual HighSchool? School { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
