using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public partial class Semester
    {
        public int SemesterId { get; set; }

        public int SchoolYearId { get; set; }

        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual SchoolYear SchoolYear { get; set; } = null!;

    }
}
