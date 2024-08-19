using Domain.Entity;
using Domain.Enums.Status;
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
                .Include(s => s.StudentInClass)
                    .ThenInclude(c => c.Class)
                    .ThenInclude(c => c.SchoolYear)
                .ToListAsync();
            return stuSupervisors;
        }

        public async Task<StudentSupervisor> GetStudentSupervisorById(int id)
        {
            return _context.StudentSupervisors
              .Include(c => c.User)
              .Include(s => s.StudentInClass)
                    .ThenInclude(c => c.Class)
                    .ThenInclude(c => c.SchoolYear)
              .FirstOrDefault(s => s.StudentSupervisorId == id);
        }

        public async Task<List<StudentSupervisor>> GetStudentSupervisorsBySchoolId(int schoolId)
        {
            return await _context.StudentSupervisors
                .Include(s => s.StudentInClass)
                    .ThenInclude(c => c.Class)
                    .ThenInclude(c => c.SchoolYear)
                .Include(v => v.User)
                .Where(v => v.User.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<StudentSupervisor> GetStudentSupervisorByUserId(int userId)
        {
            return await _context.StudentSupervisors
                .Include(s => s.StudentInClass)
                    .ThenInclude(c => c.Class)
                    .ThenInclude(c => c.SchoolYear)
                .Include(ss => ss.PatrolSchedules)
                .FirstOrDefaultAsync(ss => ss.UserId == userId);
        }

        public async Task<List<StudentSupervisor>> GetActiveStudentSupervisorsWithLessThanTwoSchedules(int schoolId)
        {
            var supervisors = await _context.StudentSupervisors
                .Include(s => s.StudentInClass)
                    .ThenInclude(c => c.Class)
                    .ThenInclude(c => c.SchoolYear)
                .Include(ss => ss.User)
                .Include(ss => ss.PatrolSchedules)
                .Where(ss => ss.User.Status == UserStatusEnums.ACTIVE.ToString() &&
                             ss.User.SchoolId == schoolId &&
                             ss.PatrolSchedules.Count(ps => ps.Status == PatrolScheduleStatusEnums.ONGOING.ToString()) < 2)
                .ToListAsync();

            return supervisors;
        }

    }
}
