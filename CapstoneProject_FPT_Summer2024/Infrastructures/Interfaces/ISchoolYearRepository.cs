using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ISchoolYearRepository : IGenericRepository<SchoolYear>
    {
        Task<List<SchoolYear>> GetAllSchoolYears();
        Task<SchoolYear> GetSchoolYearById(int id);
        Task<SchoolYear> GetSchoolYearBySchoolIdAndYear(int schoolId, short year);
        Task<SchoolYear> GetOngoingSchoolYearBySchoolIdAndYear(int schoolId, short year);
        Task<List<SchoolYear>> SearchSchoolYears(short? year, DateTime? startDate, DateTime? endDate);
        Task<List<SchoolYear>> GetSchoolYearBySchoolId(int schoolId);
        Task<SchoolYear> GetYearBySchoolYearId(int schoolId, short year);
    }
}
