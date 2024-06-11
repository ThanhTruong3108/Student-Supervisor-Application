using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructures.Repository
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            var teachers = await _context.Teachers
                .Include(c => c.User)
                .Include(c => c.School)
                .ToListAsync();
            return teachers;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return _context.Teachers
               .Include(c => c.User)
               .Include(c => c.School)
               .FirstOrDefault(s => s.TeacherId == id);
        }

        public async Task<List<Teacher>> SearchTeachers(int? schoolId, int? userId, bool sex)
        {
            var query = _context.Teachers.AsQueryable();

            if (schoolId.HasValue)
            {
                query = query.Where(p => p.SchoolId == schoolId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(p => p.UserId == userId.Value);
            }

            query = query.Where(p => p.Sex == sex);

            return await query
                .Include(c => c.User)
                .Include(c => c.School)
                .ToListAsync();
        }
    }
}
