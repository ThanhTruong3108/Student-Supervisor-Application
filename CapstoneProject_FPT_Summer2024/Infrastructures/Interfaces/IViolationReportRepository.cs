using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IViolationReportRepository : IGenericRepository<ViolationReport>
    {
        Task<List<ViolationReport>> GetAllVioReports();
        Task<ViolationReport> GetVioReportById(int id);
        Task<ViolationReport> GetVioReportByVioId(int violationId);
        Task<List<ViolationReport>> SearchVioReports(int? studentInClassId, int? violationId);
    }
}
