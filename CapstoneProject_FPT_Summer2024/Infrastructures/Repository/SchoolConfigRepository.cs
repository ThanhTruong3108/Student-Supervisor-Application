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
    public class SchoolConfigRepository : GenericRepository<SchoolConfig>, ISchoolConfigRepository
    {
        public SchoolConfigRepository(SchoolRulesContext context) : base(context) { }








    }
}
