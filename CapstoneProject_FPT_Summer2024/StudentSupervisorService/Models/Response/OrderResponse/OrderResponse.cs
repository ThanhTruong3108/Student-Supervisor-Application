using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.OrderResponse
{
    public class OrderResponse
    {
        public int OrderCode { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public int? AmountPaid { get; set; }
        public int? AmountRemaining { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
