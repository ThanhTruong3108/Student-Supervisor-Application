using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.StudentInClassRequest
{
    public class StudentInClassCreateRequest
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime? EnrollDate { get; set; }
        public bool IsSupervisor { get; set; }
        public string? Status { get; set; }
    }

    public class StudentInClassUpdateRequest
    {
        public int StudentInClassId { get; set; }
        public int? ClassId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? EnrollDate { get; set; }
        public bool? IsSupervisor { get; set; }
        public string? Status { get; set; }
    }
}
