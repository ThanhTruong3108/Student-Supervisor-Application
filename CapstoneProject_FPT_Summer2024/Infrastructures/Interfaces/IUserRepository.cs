using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetActiveSchoolAdminBySchoolId(int schoolId);
        Task<User> GetAccountByPhone(string phone);
        Task<List<User>> GetUsersBySchoolId(int schoolId);
        Task<List<User>> GetActiveStudentSupervisorBySchoolId(int schoolId);
        Task UpdateMultipleUsers(List<User> users);
    }
}
