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
    public class RegisteredSchoolRepository : GenericRepository<RegisteredSchool>, IRegisteredSchoolRepository
    {
        public RegisteredSchoolRepository(SchoolRulesContext context): base(context) { }





    }
}
