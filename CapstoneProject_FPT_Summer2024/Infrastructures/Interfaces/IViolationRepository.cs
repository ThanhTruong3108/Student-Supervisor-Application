using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IViolationRepository : IGenericRepository<Violation>
    {
        Task<List<Violation>> GetAllViolations();
        Task<Violation> GetViolationById(int id);
        Task<List<Violation>> SearchViolations(int? classId, int? teacherId, int? vioTypeId, string? code, string? name, DateTime? date);
    }
}
