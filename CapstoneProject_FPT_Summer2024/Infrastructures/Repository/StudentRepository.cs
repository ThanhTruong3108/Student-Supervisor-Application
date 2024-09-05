using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students
                .Include(s => s.School)
                .ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students
                .Include(s => s.School)
                .FirstOrDefaultAsync(x => x.StudentId == id);
        }

        // get the number of students in a school by schoolId
        public async Task<int> GetNumberOfStudentsBySchoolId(int schoolId)
        {
            return await _context.Students
                .Where(s => s.SchoolId == schoolId)
                .CountAsync();
        }

        public async Task<Student> CreateStudent(Student studentEntity)
        {
            await _context.Students.AddAsync(studentEntity);
            await _context.SaveChangesAsync();
            return studentEntity;
        }

        public async Task<Student> UpdateStudent(Student studentEntity)
        {
            _context.Entry(studentEntity).CurrentValues.SetValues(studentEntity);
            await _context.SaveChangesAsync();
            return studentEntity;
        }

        public async Task DeleteStudent(int id)
        {
            try
            {
                var studentEntity = await _context.Students.FindAsync(id);
                if (studentEntity != null)
                {
                    _context.Students.Remove(studentEntity);
                    await _context.SaveChangesAsync();
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message + (ex.InnerException != null ? ex.InnerException.Message : ""));
            }
        }

        public async Task<Student> GetStudentByCode(string code)
        {
            return _context.Students
                .Include(s => s.School)
                .FirstOrDefault(x => x.Code == code);
        }

        public async Task<Student> GetStudentByCodeAndSchoolId(string code, int schoolId)
        {
            return _context.Students
                .Include(s => s.School)
                .FirstOrDefault(x => x.Code == code && x.SchoolId == schoolId);
        }

        public async Task<List<Student>> GetStudentsBySchoolId(int schoolId)
        {
            return await _context.Students
                .Include(c => c.School)
                .Where(u => u.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task ImportExcel(List<Student> students)
        {
            await _context.Students.AddRangeAsync(students);
            await _context.SaveChangesAsync();
        }
    }
}
