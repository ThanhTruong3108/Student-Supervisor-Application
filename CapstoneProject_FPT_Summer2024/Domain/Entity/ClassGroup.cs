using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class ClassGroup
{
    public int ClassGroupId { get; set; }

    public string ClassGroupName { get; set; } = null!;

    public string Hall { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();
}
