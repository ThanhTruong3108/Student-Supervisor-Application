using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class StudentInClass
{
    public int StudentInClassId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public DateTime? EnrollDate { get; set; }

    public bool? IsSupervisor { get; set; }

    public string? Status { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<ViolationReport> ViolationReports { get; set; } = new List<ViolationReport>();
}
