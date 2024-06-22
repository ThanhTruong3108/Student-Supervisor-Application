using Domain.Entity;
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
    public class ViolationReportRepository : GenericRepository<ViolationReport>, IViolationReportRepository
    {
        public ViolationReportRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<ViolationReport>> GetAllVioReports()
        {
            var vioReports = await _context.ViolationReports
                .Include(c => c.StudentInClass)
                .Include(c => c.Violation)
                .ToListAsync();
            return vioReports;
        }
        
        public async Task<ViolationReport> GetVioReportById(int id)
        {
            return _context.ViolationReports
               .Include(c => c.StudentInClass)
               .Include(c => c.Violation)
               .FirstOrDefault(s => s.ViolationReportId == id);
        }

        public async Task<ViolationReport> GetVioReportByVioId(int violationId)
        {
            return await _context.ViolationReports.FirstOrDefaultAsync(s => s.ViolationId == violationId);
        }

        public async Task<List<ViolationReport>> SearchVioReports(int? studentInClassId, int? violationId)
        {
            var query = _context.ViolationReports.AsQueryable();

            if (studentInClassId.HasValue)
            {
                query = query.Where(p => p.StudentInClassId == studentInClassId.Value);
            }

            if (violationId.HasValue)
            {
                query = query.Where(p => p.ViolationId == violationId.Value);
            }

            return await query
                .Include(c => c.StudentInClass)
                .Include(c => c.Violation)
                .ToListAsync();
        }
    }
}
