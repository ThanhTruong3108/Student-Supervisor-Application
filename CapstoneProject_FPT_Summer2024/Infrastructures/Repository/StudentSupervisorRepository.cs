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
    public class StudentSupervisorRepository : GenericRepository<StudentSupervisor>, IStudentSupervisorRepository
    {
        public StudentSupervisorRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<StudentSupervisor>> GetAllStudentSupervisors()
        {
            var stuSupervisors = await _context.StudentSupervisors
                .Include(c => c.User)
                .ToListAsync();
            return stuSupervisors;
        }

        public async Task<StudentSupervisor> GetStudentSupervisorById(int id)
        {
            return _context.StudentSupervisors
              .Include(c => c.User)
              .FirstOrDefault(s => s.StudentSupervisorId == id);
        }

        public async Task<List<StudentSupervisor>> SearchStudentSupervisors(int? userId, string code)
        {
            var query = _context.StudentSupervisors.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(p => p.UserId == userId.Value);
            }

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }

            return await query
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
