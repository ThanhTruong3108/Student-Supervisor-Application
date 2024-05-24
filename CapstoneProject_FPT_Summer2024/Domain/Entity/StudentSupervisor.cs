using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class StudentSupervisor
{
    public int StudentSupervisorId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<PatrolSchedule> PatrolSchedules { get; set; } = new List<PatrolSchedule>();

    public virtual User User { get; set; } = null!;
}
