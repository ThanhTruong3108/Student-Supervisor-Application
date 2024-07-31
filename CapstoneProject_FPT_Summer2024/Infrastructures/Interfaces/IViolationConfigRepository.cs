using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IViolationConfigRepository : IGenericRepository<ViolationConfig>
    {
        Task<List<ViolationConfig>> GetAllViolationConfigs();
        Task<ViolationConfig> GetViolationConfigById(int id);
        Task<List<ViolationConfig>> GetViolationConfigsBySchoolId(int schoolId);
        Task<ViolationConfig> GetConfigByViolationTypeId(int violationTypeId);
        Task<List<ViolationConfig>> GetActiveViolationConfigsBySchoolId(int schoolId);
    }
}
