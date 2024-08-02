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
    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        public DisciplineRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Discipline>> GetAllDisciplines()
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .ToListAsync();
        }

        public async Task<Discipline> GetDisciplineById(int id)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .FirstOrDefaultAsync(x => x.DisciplineId == id);
        }

        public async Task<Discipline> CreateDiscipline(Discipline disciplineEntity)
        {
            await _context.Disciplines.AddAsync(disciplineEntity);
            await _context.SaveChangesAsync();
            return disciplineEntity;
        }

        public async Task<Discipline> UpdateDiscipline(Discipline disciplineEntity)
        {
            _context.Disciplines.Update(disciplineEntity);
            await _context.SaveChangesAsync();
            return disciplineEntity;
        }

        public async Task DeleteDiscipline(int id)
        {
            var disciplineEntity = await _context.Disciplines.FindAsync(id);
            disciplineEntity.Status = DisciplineStatusEnums.INACTIVE.ToString();
            _context.Entry(disciplineEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Discipline>> GetDisciplinesBySchoolId(int schoolId)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .Where(v => v.Pennalty.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<Discipline> GetDisciplineByViolationId(int violationId)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .FirstOrDefaultAsync(x => x.ViolationId == violationId);
        }

        public async Task<List<Discipline>> GetDisciplinesByUserId(int userId)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(v => v.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(d => d.Violation)
                    .ThenInclude(v => v.StudentInClass)
                        .ThenInclude(sic => sic.Student) 
                .Include(d => d.Pennalty)
                .Where(d => d.Violation.Class.Teacher.UserId == userId)
                .ToListAsync();
        }

    }
}
