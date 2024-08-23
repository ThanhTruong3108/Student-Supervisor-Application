using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SchoolRulesContext context) : base(context) { }

        public async Task<User> GetAccountByPhone(string phone)
        {
            return _context.Users
                .Include(r => r.Role)
                .Include(s => s.School)
                .FirstOrDefault(u => u.Phone.Equals(phone));
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(c => c.School)
                .Include(c => c.Role)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            return _context.Users
               .Include(c => c.School)
               .Include(c => c.Role)
               .FirstOrDefault(s => s.UserId == id);
        }

        public async Task<User> GetActiveSchoolAdminBySchoolId(int schoolId)
        {
            return await _context.Users
                .Include(c => c.Role)
                .Include(c => c.School)
                .FirstOrDefaultAsync(u => u.SchoolId == schoolId 
                                    && u.Status.Equals(UserStatusEnums.ACTIVE.ToString())    
                                    && u.Role.RoleName.Equals(RoleEnum.SCHOOL_ADMIN.ToString()));
        }

        // lấy các ACTIVE user có role là STUDENT_SUPERVISOR theo schoolId
        public async Task<List<User>> GetActiveStudentSupervisorBySchoolId(int schoolId)
        {
            return await _context.Users
                .Include(c => c.Role)
                .Include(c => c.School)
                .Where(u => u.SchoolId == schoolId
                            && u.Status.Equals(UserStatusEnums.ACTIVE.ToString())
                            && u.Role.RoleName.Equals(RoleEnum.STUDENT_SUPERVISPOR.ToString()))
                .ToListAsync();
        }

        public async Task<List<User>> SearchUsers(int? schoolId, int? role, string? code, string? name, string? phone)
        {
            var query = _context.Users.AsQueryable();

            if (schoolId.HasValue)
            {
                query = query.Where(p => p.SchoolId == schoolId.Value);
            }

            if (role.HasValue)
            {
                query = query.Where(p => p.RoleId == role.Value);
            }

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(p => p.Phone.Contains(phone));
            }

            return await query
                .Include(c => c.School)
                .Include (c => c.Role)
                .ToListAsync();
        }
        public async Task<List<User>> GetUsersBySchoolId(int schoolId)
        {
            return await _context.Users
                .Include(c => c.School)
                .Include(c => c.Role)
                .Where(u => u.SchoolId == schoolId)
                .ToListAsync();
        }

        // update nhiều User
        public async Task UpdateMultipleUsers(List<User> users)
        {
            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync();
        }
    }
}
