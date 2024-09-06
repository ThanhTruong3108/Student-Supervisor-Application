using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IClassReposirory : IGenericRepository<Class>
    {
        Task<List<Class>> GetAllClasses();
        Task<List<Class>> GetAllActiveClasses();
        Task<List<Class>> GetActiveClassesBySchoolYearId();
        Task<List<Class>> GetActiveClassesBySchoolId(int schoolId);
        Task<Class> GetClassById(int id);
        Task<Class> CreateClass(Class classEntity);
        Task<Class> UpdateClass(Class classEntity);
        Task DeleteClass(int id);
        Task<List<Class>> GetClasssBySchoolId(int schoolId);
        Task<Class> GetClassByScheduleId(int scheduleId);
        Task<List<Class>> GetClassesByUserId(int userId);
        Task<List<Class>> GetClassesBySupervisorId(int userId);
        Task<List<Class>> GetClassesByTeacherAndYear(int userId, short year);
    }
}
