using Domain.Entity;
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
    public class ViolationRepository : GenericRepository<Violation>, IViolationRepository
    {
        public ViolationRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Violation>> GetAllViolations()
        {
            var violations = await _context.Violations
                .Include(v => v.ViolationType)
                    .ThenInclude(vt => vt.ViolationGroup)
                .Include(v => v.Teacher)
                .Include(v => v.ViolationReports)
                    .ThenInclude(vr => vr.StudentInClass)
                        .ThenInclude(sic => sic.Student)
                .ToListAsync();

            return violations;
        }

        public async Task<Violation> GetViolationById(int id)
        {
            return _context.Violations
               .Include(c => c.Class)
               .Include(c => c.ViolationType)
               .Include(c => c.Teacher)
               .Include(v => v.ViolationReports)
                    .ThenInclude(vr => vr.StudentInClass)
                        .ThenInclude(sic => sic.Student)
               .FirstOrDefault(s => s.ViolationId == id);
        }

        public async Task<List<Violation>> SearchViolations(int? classId, int? teacherId, int? vioTypeId, string? code, string? name, DateTime? date)
        {
            var query = _context.Violations.AsQueryable();

            if (classId.HasValue)
            {
                query = query.Where(p => p.ClassId == classId.Value);
            }

            if (teacherId.HasValue)
            {
                query = query.Where(p => p.TeacherId == teacherId.Value);
            }

            if (vioTypeId.HasValue)
            {
                query = query.Where(p => p.ViolationTypeId == vioTypeId.Value);
            }

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (date.HasValue)
            {
                query = query.Where(p => p.Date >= date.Value);
            }

            return await query
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                .Include(c => c.Teacher)
                .Include(v => v.ViolationReports)
                    .ThenInclude(vr => vr.StudentInClass)
                        .ThenInclude(sic => sic.Student)
                .ToListAsync();
        }
    }
}
