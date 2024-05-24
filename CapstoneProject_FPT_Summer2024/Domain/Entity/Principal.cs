using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Principal
{
    public int PrincipalId { get; set; }

    public int? SchoolId { get; set; }

    public int? UserId { get; set; }

    public virtual HighSchool? School { get; set; }

    public virtual User? User { get; set; }
}
