using Domain.Entity;
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
    public class RequestOfViolation
    {
        public RequestOfViolation(int classId, int violationTypeId, int teacherId, string code, string violationName, string description, DateTime date, DateTime createdAt, int createdBy, DateTime updatedAt, int updatedBy)
        {
            ClassId = classId;
            ViolationTypeId = violationTypeId;
            TeacherId = teacherId;
            Code = code;
            ViolationName = violationName;
            Description = description;
            Date = date;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;

        }
        [Required(ErrorMessage = "The ClassId field is required.")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }
        [Required(ErrorMessage = "The TeacherId field is required.")]
        public int? TeacherId { get; set; }
        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "The ViolationName field is required.")]
        public string ViolationName { get; set; } = null!;
        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The Date field is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "The CreatedAt field is required.")]
        public DateTime? CreatedAt { get; set; }
        [Required(ErrorMessage = "The CreatedBy field is required.")]
        public int? CreatedBy { get; set; }
        [Required(ErrorMessage = "The UpdatedAt field is required.")]
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "The UpdatedBy field is required.")]
        public int? UpdatedBy { get; set; }
    }
}
