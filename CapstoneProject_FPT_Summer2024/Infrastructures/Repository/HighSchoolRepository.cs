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
    public class HighSchoolRepository : GenericRepository<HighSchool>, IHighSchoolRepository
    {
        public HighSchoolRepository(SchoolRulesContext context) : base (context) { }
        public async Task<List<HighSchool>> GetAllHighSchools()
        {
            var highSchools = await _context.HighSchools
                .ToListAsync();
            return highSchools;
        }

        public async Task<HighSchool> GetHighSchoolById(int id)
        {
            return _context.HighSchools.FirstOrDefault(r => r.SchoolId == id);
        }

        public async Task<List<HighSchool>> SearchHighSchools(string? code, string? name, string? address, string? phone)
        {
            var query = _context.HighSchools.AsQueryable();

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(p => p.Address.Contains(address));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(p => p.Phone.Contains(phone));
            }
            
            return await query.ToListAsync();
        }
    }
}
