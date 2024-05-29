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
    public class SchoolYearRepository : GenericRepository<SchoolYear>, ISchoolYearRepository
    {
        public SchoolYearRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<SchoolYear>> GetAllSchoolYears()
        {
            var schoolYears = await _context.SchoolYears
                .Include(c => c.School)
                .ToListAsync();
            return schoolYears;
        }

        public async Task<SchoolYear> GetSchoolYearById(int id)
        {
            return _context.SchoolYears
               .Include(c => c.School)
               .FirstOrDefault(s => s.SchoolYearId == id);
        }
    }
}
