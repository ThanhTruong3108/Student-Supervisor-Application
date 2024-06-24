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
            return await _context.StudentInClasses.Include(s => s.Student).ToListAsync();
        }

        public async Task<StudentInClass> GetStudentInClassById(int id)
        {
            return await _context.StudentInClasses.Include(s => s.Student).FirstOrDefaultAsync(x => x.StudentInClassId == id);
        }

        public async Task<List<StudentInClass>> SearchStudentInClass(int? classId, int? studentId, DateTime? enrollDate, bool? isSupervisor, string? status)
        {
            var query = _context.StudentInClasses.AsQueryable();

            if (classId != null)
            {
                query = query.Where(p => p.ClassId == classId);
            }
            if (studentId != null)
            {
                query = query.Where(p => p.StudentId == studentId);
            }
            if (enrollDate != null)
            {
                query = query.Where(p => p.EnrollDate == enrollDate);
            }
            if (isSupervisor != null)
            {
                query = query.Where(p => p.IsSupervisor == isSupervisor);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.Contains(status));
            }

            return await query.Include(s => s.Student).ToListAsync();
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
            studentInClassEntity.Status = StudentInClassStatusEnums.INACTIVE.ToString();
            _context.Entry(studentInClassEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
