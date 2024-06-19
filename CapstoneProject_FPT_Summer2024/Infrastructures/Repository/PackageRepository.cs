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
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Package>> GetAllPackages()
        {
            var packages = await _context.Packages
                .ToListAsync();
            return packages;
        }

        public async Task<Package> GetPackageById(int id)
        {
            return _context.Packages
                .FirstOrDefault(v => v.PackageId == id);
        }

        public async Task<List<Package>> SearchPackages(string? name, int? minPrice, int? maxPrice, string? Type)
        {
            var query = _context.Packages.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }


            if (!string.IsNullOrEmpty(Type))
            {
                query = query.Where(p => p.Type.Contains(Type));
            }

            return await query.ToListAsync();
        }
    }
}
