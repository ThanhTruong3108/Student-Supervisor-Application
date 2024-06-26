using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class SchoolAdminRepository : GenericRepository<SchoolAdmin>, ISchoolAdminRepository
    {
        public SchoolAdminRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<SchoolAdmin>> GetAllSchoolAdmins()
        {
            var schoolAdmins = await _context.SchoolAdmins
                .Include(c => c.School)
                .Include(c => c.Admin)
                .ToListAsync();
            return schoolAdmins;
        }

        public async Task<SchoolAdmin> GetBySchoolAdminId(int id)
        {
            return _context.SchoolAdmins
               .Include(c => c.School)
               .Include(c => c.Admin)
               .FirstOrDefault(s => s.SchoolAdminId == id);
        }

        public async Task<List<SchoolAdmin>> SearchSchoolAdmins(int? schoolId, int? adminId)
        {
            var query = _context.SchoolAdmins.AsQueryable();

            if (schoolId.HasValue)
            {
                query = query.Where(p => p.SchoolId == schoolId.Value);
            }

            if (adminId.HasValue)
            {
                query = query.Where(p => p.AdminId == adminId.Value);
            }


            return await query
                .Include(c => c.School)
                .Include(c => c.Admin)
                .ToListAsync();
        }
    }
}
