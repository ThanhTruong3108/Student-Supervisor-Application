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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructures.Repository
{
    public class ViolationTypeRepository : GenericRepository<ViolationType>, IViolationTypeRepository
    {
        public ViolationTypeRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<ViolationType>> GetAllVioTypes()
        {
            var vioTypes = await _context.ViolationTypes
                .Include(v => v.ViolationGroup)
                .ToListAsync();
            return vioTypes;
        }

        public async Task<List<ViolationType>> GetViolationTypesBySchoolId(int schoolId)
        {
            return await _context.ViolationTypes
                .Include(v => v.ViolationGroup)
                .Where(v => v.ViolationGroup.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<ViolationType> GetVioTypeById(int id)
        {
            return await _context.ViolationTypes
                .Include(v => v.ViolationGroup)
                .FirstOrDefaultAsync(v => v.ViolationTypeId == id);
        }

        public async Task<List<ViolationType>> GetActiveViolationTypesBySchoolId(int schoolId)
        {
            return await _context.ViolationTypes
                .Include(v => v.ViolationGroup)
                .Where(v => v.ViolationGroup.SchoolId == schoolId && v.Status == ViolationTypeStatusEnums.ACTIVE.ToString())
                .ToListAsync();
        }
    }
}
