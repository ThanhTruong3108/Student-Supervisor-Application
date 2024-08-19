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
    public class StudentInClassRepository : GenericRepository<StudentInClass>, IStudentInClassRepository
    {
        public StudentInClassRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<StudentInClass>> GetAllStudentInClass()
        {
            return await _context.StudentInClasses
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .Include(s => s.Student)
                .ToListAsync();
        }

        public async Task<StudentInClass> GetStudentInClassById(int id)
        {
            return await _context.StudentInClasses
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(x => x.StudentInClassId == id);
        }
        public async Task<StudentInClass> CreateStudentInClass(StudentInClass studentInClassEntity)
        {
            await _context.StudentInClasses.AddAsync(studentInClassEntity);
            await _context.SaveChangesAsync();
            return studentInClassEntity;
        }

        public async Task<StudentInClass> UpdateStudentInClass(StudentInClass studentInClassEntity)
        {
            _context.StudentInClasses.Update(studentInClassEntity);
            await _context.SaveChangesAsync();
            return studentInClassEntity;
        }

        public async Task DeleteStudentInClass(int id)
        {
            var studentInClassEntity = await _context.StudentInClasses.FindAsync(id);
            studentInClassEntity.Status = StudentInClassStatusEnums.UNENROLLED.ToString();
            _context.Entry(studentInClassEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsStudentEnrolledInAnyClass(int studentId)
        {
            return await _context.StudentInClasses.AnyAsync(sic => sic.StudentId == studentId && sic.Status == StudentInClassStatusEnums.ENROLLED.ToString());
        }

        public async Task<List<StudentInClass>> GetStudentInClassesBySchoolId(int schoolId)
        {
            return await _context.StudentInClasses
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .Include(s => s.Student)
                .Where(v => v.Student.SchoolId == schoolId)
                .ToListAsync();
        }
    }
}
