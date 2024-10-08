﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentSupervisorService.Models.Request.ViolationConfigRequest
{
    public class RequestOfViolationConfig
    {

        [Required(ErrorMessage = "The MinusPoints field is required")]
        [Range(1, 30, ErrorMessage = "Điểm trừ phải là số dương từ 1 đến 30")]
        public int? MinusPoints { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }
    }
}
