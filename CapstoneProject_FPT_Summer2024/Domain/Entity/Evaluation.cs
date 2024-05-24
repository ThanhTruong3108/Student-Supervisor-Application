using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int SchoolYearId { get; set; }

    public string? Description { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public short Point { get; set; }

    public virtual SchoolYear SchoolYear { get; set; } = null!;

    public virtual ICollection<ViolationConfig> ViolationConfigs { get; set; } = new List<ViolationConfig>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
