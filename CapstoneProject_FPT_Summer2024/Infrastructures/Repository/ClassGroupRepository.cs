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
    public class ClassGroupRepository : GenericRepository<ClassGroup>, IClassGroupRepository
    {
        public ClassGroupRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<ClassGroup>> GetAllClassGroups()
        {
            return await _context.ClassGroups.ToListAsync();
        }

        public async Task<ClassGroup> GetClassGroupById(int id)
        {
            return await _context.ClassGroups.FirstOrDefaultAsync(x => x.ClassGroupId == id);
        }

        public async Task<List<ClassGroup>> SearchClassGroups(string? name, string? hall, string? status)
        {
            var query = _context.ClassGroups.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ClassGroupName.Contains(name));
            }
            if (!string.IsNullOrEmpty(hall))
            {
                query = query.Where(p => p.Hall.Contains(hall));
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.Contains(status));
            }

            return await query.ToListAsync();
        }

        public async Task<ClassGroup> CreateClassGroup(ClassGroup classGroupEntity)
        {
            await _context.ClassGroups.AddAsync(classGroupEntity);
            await _context.SaveChangesAsync();
            return classGroupEntity;
        }

        public async Task<ClassGroup> UpdateClassGroup(ClassGroup classGroupEntity)
        {
            _context.ClassGroups.Update(classGroupEntity);
            await _context.SaveChangesAsync();
            return classGroupEntity;
        }
        public async Task DeleteClassGroup(int id)
        {
            var classGroupEntity = await _context.ClassGroups.FindAsync(id);
            classGroupEntity.Status = ClassGroupStatusEnums.INACTIVE.ToString();
            _context.Entry(classGroupEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
