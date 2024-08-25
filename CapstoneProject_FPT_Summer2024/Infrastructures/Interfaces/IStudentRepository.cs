using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByCode(string code);
        Task<Student> GetStudentByCodeAndSchoolId(string code, int schoolId);
        Task<int> GetNumberOfStudentsBySchoolId(int schoolId);
        Task<Student> CreateStudent(Student studentEntity);
        Task<Student> UpdateStudent(Student studentEntity);
        Task DeleteStudent(int id);
        Task<List<Student>> GetStudentsBySchoolId(int schoolId);
    }
}
