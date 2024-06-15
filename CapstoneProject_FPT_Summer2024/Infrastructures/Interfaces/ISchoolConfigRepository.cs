using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ISchoolConfigRepository : IGenericRepository<SchoolConfig>
    {
        Task<List<SchoolConfig>> GetAllSchoolConfigs();
        Task<SchoolConfig> GetSchoolConfigById(int id);
        Task<List<SchoolConfig>> SearchSchoolConfigs(int? schoolId, string? name, string? code, string? description, string? status);
        Task<SchoolConfig> CreateSchoolConfig(SchoolConfig schoolConfigEntity);
        Task<SchoolConfig> UpdateSchoolConfig(SchoolConfig schoolConfigEntity);
        Task DeleteSchoolConfig(int id);
    }
}
