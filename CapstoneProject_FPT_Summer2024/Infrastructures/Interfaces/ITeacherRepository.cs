using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int id);
        Task<Teacher> GetTeacherByUserId(int id);
        Task<Teacher> GetTeacherByIdWithUser(int id);
        Task<List<Teacher>> GetTeachersBySchoolId(int schoolId);
        Task<List<Teacher>> GetAllTeachersWithRoleTeacher(int schoolId);
        Task<List<Teacher>> GetAllTeachersWithRoleSupervisor(int schoolId);
    }
}
