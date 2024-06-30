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

        [Required(ErrorMessage = "The SchoolId field is required.")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "The TeacherName field is required.")]
        public string TeacherName { get; set; }
        [RegularExpression(@"^[0-9]{8,9}$", ErrorMessage = "The phone number must be an 8 or 9 digit integer.")]
        [Required(ErrorMessage = "The SchoolName field is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Sex field is required.")]
        public bool Sex { get; set; }
        [Required(ErrorMessage = "The Address field is required.")]
        public string? Address { get; set; }

    }
}
