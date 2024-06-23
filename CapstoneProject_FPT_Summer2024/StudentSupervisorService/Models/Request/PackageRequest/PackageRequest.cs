using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.PackageRequest
{
    public class PackageRequest
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The RegisteredDate field is required.")]
        public DateTime? RegisteredDate { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "The Type field is required.")]
        public string? Type { get; set; }
    }
}
