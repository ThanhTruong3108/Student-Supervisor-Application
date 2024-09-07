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
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Semester>> GetSemestersBySchoolId(int schoolId)
        {
            return await _context.Semesters
                .Include(s => s.SchoolYear)
                .Where(s => s.SchoolYear.SchoolId == schoolId)
                .ToListAsync();
        }
        public async Task<Semester> GetSemesterById(int id)
        {
            return _context.Semesters
               .Include(c => c.SchoolYear)
               .FirstOrDefault(s => s.SemesterId == id);
        }

        public async Task<List<Semester>> GetSemestersBySchoolYearId(int schoolYearId)
        {
            return await _context.Semesters
                .Where(s => s.SchoolYearId == schoolYearId)
                .ToListAsync();
        }
    }
}
