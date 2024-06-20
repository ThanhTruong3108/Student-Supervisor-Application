using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ISchoolAdminRepository : IGenericRepository<SchoolAdmin>
    {
        Task<List<SchoolAdmin>> GetAllSchoolAdmins();
        Task<SchoolAdmin> GetBySchoolAdminId(int schoolAdminId);
        Task<SchoolAdmin> GetByUserId(int userId);
        Task<List<SchoolAdmin>> SearchSchoolAdmins(int? schoolId, int? userId);
        Task<SchoolAdmin> CreateSchoolAdmin(SchoolAdmin schoolAdminEntity);
        Task<SchoolAdmin> UpdateSchoolAdmin(SchoolAdmin schoolAdminEntity);
        Task DeleteSchoolAdmin(int id);
    }
}
