using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class ViolationReportRepository : GenericRepository<ViolationReport>, IViolationReportRepository
    {
        public ViolationReportRepository(SchoolRulesContext context): base(context) { } 





    }
}
