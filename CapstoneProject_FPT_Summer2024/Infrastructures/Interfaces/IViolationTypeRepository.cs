using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IViolationTypeRepository : IGenericRepository<ViolationType>
    {
        Task<List<ViolationType>> GetAllVioTypes();
        Task<ViolationType> GetVioTypeById(int id);
        Task<List<ViolationType>> GetViolationTypesBySchoolId(int schoolId);
        Task<List<ViolationType>> GetActiveViolationTypesBySchoolId(int schoolId);
    }
}
