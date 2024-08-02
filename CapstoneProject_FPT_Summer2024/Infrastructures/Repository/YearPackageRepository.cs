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
    public class YearPackageRepository : GenericRepository<YearPackage>, IYearPackageRepository
    {
        public YearPackageRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<YearPackage>> GetAllYearPackages()
        {
            var yearPackage = await _context.YearPackages
                .Include(s => s.SchoolYear)
                    .ThenInclude(h => h.School)
                .Include(s => s.Package)
                .ToListAsync();
            return yearPackage;
        }

        public async Task<YearPackage> GetYearPackageById(int id)
        {
            return _context.YearPackages
                .Include(s => s.SchoolYear)
                    .ThenInclude(h => h.School)
                .Include(s => s.Package)
                .FirstOrDefault();
        }

        public async Task<List<YearPackage>> GetYearPackagesBySchoolId(int schoolId)
        {
            return await _context.YearPackages
                .Include(s => s.SchoolYear)
                    .ThenInclude(h => h.School)
                .Include(s => s.Package)
                .Where(v => v.SchoolYear.SchoolId == schoolId)
                .ToListAsync();
        }

        // Get YearPackage with VALID Status By SchoolYearId
        public async Task<YearPackage> GetValidYearPackageBySchoolYearId(int schoolYearId)
        {
            return await _context.YearPackages
                .Include(s => s.SchoolYear)
                    .ThenInclude(h => h.School)
                .Include(s => s.Package)
                .Where(v => v.SchoolYearId == schoolYearId && v.Status == YearPackageStatusEnums.VALID.ToString())
                .FirstOrDefaultAsync();
        }

        // Get List YearPackage with VALID Status By SchoolYearId
        public async Task<List<YearPackage>> GetListValidYearPackageBySchoolYearId(int schoolYearId)
        {
            return await _context.YearPackages
                .Include(s => s.SchoolYear)
                    .ThenInclude(h => h.School)
                .Include(s => s.Package)
                .Where(v => v.SchoolYearId == schoolYearId && v.Status == YearPackageStatusEnums.VALID.ToString())
                .ToListAsync();
        }

        public async Task<List<YearPackage>> SearchYearPackages(int? schoolYearId, int? packageId)
        {
            var query = _context.YearPackages.AsQueryable();

            if (schoolYearId.HasValue)
            {
                query = query.Where(p => p.SchoolYearId == schoolYearId.Value);
            }

            if (packageId.HasValue)
            {
                query = query.Where(p => p.PackageId == packageId.Value);
            }

            return await query
                .Include(s => s.SchoolYear)
                .Include(s => s.Package)
                .ToListAsync();
        }

        public async Task<YearPackage> CreateYearPackage(YearPackage entity)
        {
            await _context.YearPackages.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
