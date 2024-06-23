using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.ViolationReportRequest
{
    public class RequestOfVioReport
    {

        [Required(ErrorMessage = "The StudentInClassId field is required.")]
        public int StudentInClassId { get; set; }
        [Required(ErrorMessage = "The ViolationId field is required.")]
        public int ViolationId { get; set; }
    }
}
