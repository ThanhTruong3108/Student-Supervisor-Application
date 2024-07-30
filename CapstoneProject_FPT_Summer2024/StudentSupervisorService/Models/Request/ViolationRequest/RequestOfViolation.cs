using Domain.Entity;
using Domain.Enums.Status;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.ViolationRequest
{
    public class RequestOfStuSupervisorCreateViolation
    {
        [Required(ErrorMessage = "The SchoolId field is required.")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "The UserId field is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Year field is required.")]
        public short Year { get; set; }

        [Required(ErrorMessage = "The ClassId field is required.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }

        [Required(ErrorMessage = "The StudentInClassId field is required.")]
        public int StudentInClassId { get; set; }
        [Required(ErrorMessage = "The ScheduleId field is required.")]
        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "The ViolationName field is required.")]
        public string ViolationName { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Date field is required.")]
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class RequestOfSupervisorCreateViolation
    {
        [Required(ErrorMessage = "The SchoolId field is required.")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "The UserId field is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Year field is required.")]
        public short Year { get; set; }

        [Required(ErrorMessage = "The ClassId field is required.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }

        [Required(ErrorMessage = "The StudentInClassId field is required.")]
        public int StudentInClassId { get; set; }

        [Required(ErrorMessage = "The ViolationName field is required.")]
        public string ViolationName { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Date field is required.")]
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class RequestOfUpdateViolationForStudentSupervisor
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int ViolationTypeId { get; set; }
        public int? StudentInClassId { get; set; }
        public int ScheduleId { get; set; }
        public string ViolationName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class RequestOfUpdateViolationForSupervisor
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int ViolationTypeId { get; set; }
        public int? StudentInClassId { get; set; }
        public string ViolationName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
