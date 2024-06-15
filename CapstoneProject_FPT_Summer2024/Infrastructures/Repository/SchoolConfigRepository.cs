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
    public class SchoolConfigRepository : GenericRepository<SchoolConfig>, ISchoolConfigRepository
    {
        public SchoolConfigRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<SchoolConfig>> GetAllSchoolConfigs()
        {
            return await _context.SchoolConfigs.ToListAsync();
        }

        public async Task<SchoolConfig> GetSchoolConfigById(int id)
        {
            return await _context.SchoolConfigs.FirstOrDefaultAsync(x => x.ConfigId == id);
        }

        public async Task<List<SchoolConfig>> SearchSchoolConfigs(int? schoolId, string? name, string? code, string? description, string? status)
        {
            var query = _context.SchoolConfigs.AsQueryable();

            if (schoolId != null)
            {
                query = query.Where(p => p.SchoolId == schoolId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }
            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.Contains(status));
            }

            return await query.ToListAsync();
        }

        public async Task<SchoolConfig> CreateSchoolConfig(SchoolConfig schoolConfigEntity)
        {
            await _context.SchoolConfigs.AddAsync(schoolConfigEntity);
            await _context.SaveChangesAsync();
            return schoolConfigEntity;
        }

        public async Task<SchoolConfig> UpdateSchoolConfig(SchoolConfig schoolConfigEntity)
        {
            _context.SchoolConfigs.Update(schoolConfigEntity);
            await _context.SaveChangesAsync();
            return schoolConfigEntity;
        }

        public async Task DeleteSchoolConfig(int id)
        {
            var schoolConfigEntity = await _context.SchoolConfigs.FindAsync(id);
            schoolConfigEntity.Status = SchoolConfigStatusEnums.INACTIVE.ToString();
            _context.Entry(schoolConfigEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
