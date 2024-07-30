using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int? ClassId { get; set; }

    public string? Description { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public int? Points { get; set; }

    public string? Status { get; set; }

    public virtual Class? Class { get; set; }
}
