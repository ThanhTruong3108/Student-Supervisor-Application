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
    public class ViolationRepository : GenericRepository<Violation>, IViolationRepository
    {
        public ViolationRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Violation>> GetAllViolations()
        {
            var violations = await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .ToListAsync();

            return violations;
        }

        public async Task<Violation> GetViolationById(int id)
        {
            return _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
               .FirstOrDefault(s => s.ViolationId == id);
        }

        public async Task<List<Violation>> SearchViolations(
                int? classId,
                int? violationTypeId,
                int? studentInClassId,
                int? teacherId,
                string? name,
                string? description,
                DateTime? date,
                string? status)
        {
            var query = _context.Violations.AsQueryable();

            if (classId.HasValue)
            {
                query = query.Where(p => p.ClassId == classId.Value);
            }
            if (violationTypeId.HasValue)
            {
                query = query.Where(p => p.ViolationTypeId == violationTypeId.Value);
            }
            if (studentInClassId.HasValue)
            {
                query = query.Where(p => p.StudentInClassId == studentInClassId.Value);
            }
            if (teacherId.HasValue)
            {
                query = query.Where(p => p.TeacherId == teacherId.Value);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }
            if (date.HasValue)
            {
                query = query.Where(p => p.Date == date.Value);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.Equals(status));
            }

            return await query
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .ToListAsync();
        }

        public async Task<Violation> CreateViolation(Violation violationEntity)
        {
            await _context.Violations.AddAsync(violationEntity);
            await _context.SaveChangesAsync();
            return violationEntity;
        }

        public async Task<Violation> UpdateViolation(Violation violationEntity)
        {
            _context.Violations.Update(violationEntity);
            _context.Entry(violationEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return violationEntity;
        }

        public async Task DeleteViolation(int id)
        {
            var violationEntity = await _context.Violations.FindAsync(id);
            violationEntity.Status = ViolationStatusEnums.INACTIVE.ToString();
            _context.Entry(violationEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Violation>> GetViolationsByStudentId(int studentId)
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId)
                .ToListAsync();

        }

        public async Task<List<Violation>> GetViolationsByStudentIdAndYear(int studentId, int schoolYearId)
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId && v.Class.SchoolYearId == schoolYearId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetViolationCountByYear(int studentId)
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId)
                .GroupBy(v => v.Class.SchoolYearId)
                .Select(g => new { SchoolYearId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.SchoolYearId, g => g.Count);
        }

        public async Task<List<Violation>> GetApprovedViolations()
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Status == ViolationStatusEnums.APPROVED.ToString())
                .ToListAsync();
        }

        public async Task<List<Violation>> GetPendingViolations()
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Status == ViolationStatusEnums.PENDING.ToString())
                .ToListAsync();
        }

        public async Task<List<Violation>> GetRejectedViolations()
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Status == ViolationStatusEnums.REJECTED.ToString())
                .ToListAsync();
        }

        public async Task<List<Violation>> GetInactiveViolations()
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Status == ViolationStatusEnums.INACTIVE.ToString())
                .ToListAsync();
        }

        public async Task<List<Violation>> GetViolationsBySchoolId(int schoolId)
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.Teacher)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(ed => ed.Teacher.School.SchoolId == schoolId)
                .ToListAsync();
        }
    }
}
