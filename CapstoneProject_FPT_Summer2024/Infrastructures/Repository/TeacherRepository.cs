using Domain.Entity;
using Domain.Enums.Role;
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

        public async Task<Teacher> GetTeacherByIdWithUser(int id)
        {
            return _context.Teachers
                .Include(c => c.User)
                .Include(c => c.School)
                .FirstOrDefault(t => t.TeacherId == id);
        }

        public async Task<Teacher> GetTeacherByUserId(int id)
        {
            return _context.Teachers
                .Include(c => c.User)
                .Include(t => t.ClassGroups)
                .Include(c => c.School)
                .FirstOrDefault(i => i.UserId == id);
        }

        public async Task<List<Teacher>> GetTeachersBySchoolId(int schoolId)
        {
            return await _context.Teachers
                .Include(c => c.User)
                .Include(c => c.School)
                .Where(u => u.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<List<Teacher>> GetAllTeachersWithRoleTeacher(int schoolId)
        {
            const byte teacherRoleId = 5; 
            return await _context.Teachers
                .Include(t => t.User) 
                .Where(t => t.User.RoleId == teacherRoleId && t.SchoolId == schoolId) 
                .ToListAsync();
        }
        public async Task<List<Teacher>> GetAllTeachersWithRoleSupervisor(int schoolId)
        {
            const byte teacherRoleId = 4; 
            return await _context.Teachers
                .Include(t => t.User) 
                .Where(t => t.User.RoleId == teacherRoleId && t.SchoolId == schoolId) 
                .ToListAsync();
        }
        public async Task<List<Teacher>> GetTeachersWithoutClass(int schoolId, short year)
        {
            var teachers = await _context.Teachers
                .Include(c => c.User)
                .Include(c => c.School)
                .Include(t => t.Classes)
                .Where(t => t.SchoolId == schoolId &&
                    t.User.RoleId == (byte)RoleAccountEnum.TEACHER &&
                    !t.Classes.Any(c => c.SchoolYear.Year == year))
                .ToListAsync();

            return teachers;
        }

    }
}
