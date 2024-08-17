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
    public class ClassRepository : GenericRepository<Class>, IClassReposirory
    {
        public ClassRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Class>> GetAllClasses()
        {
            return await _context.Classes
                .Include(s => s.SchoolYear)
                .Include(c => c.ClassGroup)
                .Include(t => t.Teacher)
                    .ThenInclude(u => u.User)
                .ToListAsync();
        }

        //get all ACTIVE classes
        public async Task<List<Class>> GetAllActiveClasses()
        {
            return await _context.Classes
                .Where(c => c.Status.Equals(ClassStatusEnums.ACTIVE.ToString()))
                .ToListAsync();
        }

        public async Task<Class> GetClassById(int id)
        {
            return await _context.Classes
                .Include(s => s.SchoolYear)
                .Include(c => c.ClassGroup)
                .Include(t => t.Teacher)
                    .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(x => x.ClassId == id);
        }

        // lấy các lớp ACTIVE thuộc SchoolYear có EndDate < DateTime.Now
        public async Task<List<Class>> GetActiveClassesBySchoolYearId()
        {
            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            return await _context.Classes
                .Where(c => c.SchoolYear.EndDate < oneDayAgo 
                       && c.Status.Equals(ClassStatusEnums.ACTIVE.ToString()))
                .ToListAsync();
        }

        public async Task<Class> CreateClass(Class classEntity)
        {
            await _context.Classes.AddAsync(classEntity);
            await _context.SaveChangesAsync();
            return classEntity;
        }

        public async Task<Class> UpdateClass(Class classEntity)
        {
            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync();
            return classEntity;
        }

        public async Task DeleteClass(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            classEntity.Status = ClassStatusEnums.INACTIVE.ToString();
            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Class>> GetClasssBySchoolId(int schoolId)
        {
            return await _context.Classes
                .Include(c => c.ClassGroup)
                .Include(t => t.Teacher)
                    .ThenInclude(u => u.User)
                .Include(v => v.SchoolYear)
                .Where(v => v.SchoolYear.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<Class> GetClassByScheduleId(int scheduleId)
        {
            return await _context.Classes
                .Include(c => c.SchoolYear)
                .Include(c => c.ClassGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                .Include(c => c.PatrolSchedules)
                .FirstOrDefaultAsync(c => c.PatrolSchedules.Any(ps => ps.ScheduleId == scheduleId));
        }

        public async Task<List<Class>> GetClassesByUserId(int userId)
        {
            return await _context.Classes
                .Include(c => c.SchoolYear)
                .Include(c => c.ClassGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                .Where(c => c.Teacher != null && c.Teacher.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Class>> GetClassesBySupervisorId(int userId)
        {
            return await _context.Classes
                .Include(s => s.SchoolYear)
                .Include(c => c.ClassGroup)
                .Include(t => t.Teacher)
                .ThenInclude(u => u.User)
            .Where(c => c.ClassGroup.Teacher.UserId == userId)
            .ToListAsync();
        }
    }
}
