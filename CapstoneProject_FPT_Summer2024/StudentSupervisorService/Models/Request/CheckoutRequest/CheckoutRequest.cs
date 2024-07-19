using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.CheckoutRequest
{
    public class CreateCheckoutRequest
    {
        [Required(ErrorMessage = "The Amount field is required.")]
        [Range(1000, int.MaxValue, ErrorMessage = "Amount must be > 1000")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "The PackageID field is required.")]
        public int PackageID { get; set; }
    }
}
