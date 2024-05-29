using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class HighSchoolRepository : GenericRepository<HighSchool>, IHighSchoolRepository
    {
        public HighSchoolRepository(SchoolRulesContext context) : base (context) { }
        public async Task<List<HighSchool>> GetAllHighSchools()
        {
            var highSchools = await _context.HighSchools
                .ToListAsync();
            return highSchools;
        }

        public async Task<HighSchool> GetHighSchoolById(int id)
        {
            return _context.HighSchools.FirstOrDefault(r => r.SchoolId == id);
        }
    }
}
