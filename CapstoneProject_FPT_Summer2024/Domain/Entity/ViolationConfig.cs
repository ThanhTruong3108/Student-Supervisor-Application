using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class ViolationConfig
{
    public int ViolationConfigId { get; set; }

    public int EvaluationId { get; set; }

    public int ViolationTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public virtual Evaluation Evaluation { get; set; } = null!;

    public virtual ViolationType ViolationType { get; set; } = null!;
}
