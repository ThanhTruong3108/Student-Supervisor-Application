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
        public PackageRepository(SchoolRulesContext context) : base(context) { }

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
    }
}
