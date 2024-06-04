﻿using Domain.Entity;
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
    public class RequestOfCreateViolation
    {
        [Required(ErrorMessage = "The ClassId field is required.")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }
        public int? TeacherId { get; set; }
        [Required(ErrorMessage = "The Code field is required.")]

        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "The ViolationName field is required.")]

        public string ViolationName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class RequestOfUpdateViolation
    {
        [Required(ErrorMessage = "The ClassId field is required.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }
        public int? TeacherId { get; set; }

        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "The ViolationName field is required.")]
        public string ViolationName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
