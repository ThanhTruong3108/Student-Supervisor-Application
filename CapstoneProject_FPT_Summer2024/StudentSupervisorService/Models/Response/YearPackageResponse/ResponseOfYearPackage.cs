﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.YearPackageResponse
{
    public class ResponseOfYearPackage
    {
        public int YearPackageId { get; set; }
        public int SchoolYearId { get; set; }
        public string Code { get; set; }
        public string SchoolName { get; set; }
        public short Year { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string? Status { get; set; }
    }
}
