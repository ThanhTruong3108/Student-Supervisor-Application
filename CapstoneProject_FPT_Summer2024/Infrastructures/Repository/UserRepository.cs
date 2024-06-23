using Domain.Entity;
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
                .FirstOrDefault(u => u.Phone.Equals(phone));
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(c => c.SchoolAdmin)
                .Include(c => c.Role)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            return _context.Users
               .Include(c => c.SchoolAdmin)
               .Include(c => c.Role)
               .FirstOrDefault(s => s.UserId == id);
        }

        public async Task<List<User>> SearchUsers(int? role, string? code, string? name, string? phone)
        {
            var query = _context.Users.AsQueryable();

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
                .Include(c => c.SchoolAdmin)
                .Include (c => c.Role)
                .ToListAsync();
        }
        public async Task<List<User>> GetUsersBySchoolAdminId(int schoolAdminId)
        {
            return await _context.Users
                .Include(c => c.SchoolAdmin)
                .Include(c => c.Role)
                .Where(u => u.SchoolAdminId == schoolAdminId)
                .ToListAsync();
        }
    }
}
