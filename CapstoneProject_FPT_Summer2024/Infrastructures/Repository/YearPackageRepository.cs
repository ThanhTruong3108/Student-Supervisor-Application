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
    public class YearPackageRepository : GenericRepository<YearPackage>, IYearPackageRepository
    {
        public YearPackageRepository(SchoolRulesContext context): base(context) { }




    }
}
