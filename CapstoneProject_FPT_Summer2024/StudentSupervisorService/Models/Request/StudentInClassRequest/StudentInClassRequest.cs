using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.StudentInClassRequest
{
    public class StudentInClassCreateRequest
    {
        // field của Student
        public int SchoolId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        // field của StudentInClass
        public int ClassId { get; set; }
        public DateTime EnrollDate { get; set; }
        public bool IsSupervisor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class StudentInClassUpdateRequest
    {
        public int StudentInClassId { get; set; } // truyền để biết hsinh nào học lớp nào cần update
        public int StudentId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? ClassId { get; set; }
        public DateTime? EnrollDate { get; set; }
        public bool? IsSupervisor { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfViolation { get; set; }
    }
}
