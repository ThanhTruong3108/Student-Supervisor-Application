using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class PatrolSchedule
{
    public int ScheduleId { get; set; }

    public int ClassId { get; set; }

    public int? UserId { get; set; }

    public int SupervisorId { get; set; }

    public string? Name { get; set; }

    public int? Slot { get; set; }

    public TimeSpan? Time { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public string Status { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual StudentSupervisor Supervisor { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
