using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class Time
{
    public int TimeId { get; set; }

    public int ClassGroupId { get; set; }

    public byte Slot { get; set; }

    public TimeSpan Time1 { get; set; }

    public virtual ClassGroup ClassGroup { get; set; } = null!;
}
