﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.StudentInClassResponse
{
    public class StudentInClassResponse
    {
        public int StudentInClassId { get; set; }
        public int ClassId { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        public bool? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? EnrollDate { get; set; }
        public bool? IsSupervisor { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfViolation { get; set; }
        public short Year { get; set; }
        public string? Status { get; set; }
    }
}
