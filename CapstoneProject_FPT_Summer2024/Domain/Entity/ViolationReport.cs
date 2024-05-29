using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class ViolationReport
{
    public int ViolationReportId { get; set; }

    public int StudentInClassId { get; set; }

    public int ViolationId { get; set; }

    public virtual StudentInClass StudentInClass { get; set; } = null!;

    public virtual Violation Violation { get; set; } = null!;
}
