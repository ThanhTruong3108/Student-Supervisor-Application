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
    public class SchoolAdminRepository : GenericRepository<SchoolAdmin>, ISchoolAdminRepository
    {
        public SchoolAdminRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<SchoolAdmin>> GetAllSchoolAdmins()
        {
            return await _context.SchoolAdmins.Include(c => c.User).ToListAsync();
        }

        public async Task<SchoolAdmin> GetBySchoolAdminId(int schoolAdminId)
        {
            return await _context.SchoolAdmins
                .Include(c => c.User)
                .FirstOrDefaultAsync(s => s.SchoolAdminId == schoolAdminId);
        }
        public async Task<SchoolAdmin> GetByUserId(int userId)
        {
            return await _context.SchoolAdmins
                .Include(c => c.User)
                .FirstOrDefaultAsync(i => i.UserId == userId);
        }

        public async Task<List<SchoolAdmin>> SearchSchoolAdmins(int? schoolId, int? userId)
        {
            var query = _context.SchoolAdmins.AsQueryable();

            if (schoolId != null)
            {
                query = query.Where(p => p.SchoolId == schoolId);
            }
            if (userId != null)
            {
                query = query.Where(p => p.UserId == userId);
            }

            return await query.Include(c => c.User).ToListAsync();
        }

        public async Task<SchoolAdmin> CreateSchoolAdmin(SchoolAdmin schoolAdminEntity)
        {
            await _context.SchoolAdmins.AddAsync(schoolAdminEntity);
            await _context.SaveChangesAsync();
            return schoolAdminEntity;
        }

        public async Task<SchoolAdmin> UpdateSchoolAdmin(SchoolAdmin schoolAdminEntity)
        {
            _context.SchoolAdmins.Update(schoolAdminEntity);
            await _context.SaveChangesAsync();
            return schoolAdminEntity;
        }

        public async Task DeleteSchoolAdmin(int id)
        {
            var schoolAdminEntity = await _context.SchoolAdmins
                .Include(sa => sa.User)
                .SingleOrDefaultAsync(sa => sa.SchoolAdminId == id);

            if (schoolAdminEntity != null)
            {
                schoolAdminEntity.User.Status = UserEnum.INACTIVE.ToString();
                _context.SchoolAdmins.Update(schoolAdminEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
