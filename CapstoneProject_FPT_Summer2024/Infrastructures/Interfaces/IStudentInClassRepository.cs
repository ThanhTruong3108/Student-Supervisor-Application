using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IStudentInClassRepository : IGenericRepository<StudentInClass>
    {
        Task<List<StudentInClass>> GetAllStudentInClass();
        Task<StudentInClass> GetStudentInClassById(int id);
        Task<StudentInClass> CreateStudentInClass(StudentInClass studentInClassEntity);
        Task<StudentInClass> UpdateStudentInClass(StudentInClass studentInClassEntity);
        Task DeleteStudentInClass(int id);
        Task<bool> IsStudentEnrolledInAnyClass(int studentId);
        Task<List<StudentInClass>> GetStudentInClassesBySchoolId(int schoolId);
    }
}
