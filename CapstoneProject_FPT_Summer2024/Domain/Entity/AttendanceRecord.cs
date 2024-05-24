using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class AttendanceRecord
{
    public int AttendanceRecordId { get; set; }

    public int StudentInClassId { get; set; }

    public int TeacherId { get; set; }

    public DateTime Date { get; set; }

    public bool IsPresent { get; set; }

    public string Status { get; set; } = null!;

    public virtual StudentInClass StudentInClass { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
