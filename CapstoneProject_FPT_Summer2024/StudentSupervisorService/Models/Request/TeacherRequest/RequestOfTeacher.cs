using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.TeacherRequest
{
    public class RequestOfTeacher
    {
        public RequestOfTeacher(int schoolId, bool sex, int userId)
        {
            SchoolId = schoolId;
            Sex = sex;
            UserId = userId;
        }
        [Required(ErrorMessage = "The SchoolId field is required.")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "The UserId field is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "The Sex field is required.")]
        public bool Sex { get; set; }
    }
}
