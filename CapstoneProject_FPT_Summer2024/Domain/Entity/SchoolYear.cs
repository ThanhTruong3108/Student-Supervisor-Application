﻿using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class SchoolYear
{
    public int SchoolYearId { get; set; }

    public int SchoolId { get; set; }

    public short Year { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual HighSchool School { get; set; } = null!;

    public virtual ICollection<YearPackage> YearPackages { get; set; } = new List<YearPackage>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
