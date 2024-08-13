using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IYearPackageRepository : IGenericRepository<YearPackage>
    {
        Task<List<YearPackage>> GetAllYearPackages();
        Task<YearPackage> GetYearPackageById(int id);
        Task<List<YearPackage>> SearchYearPackages(int? schoolYearId, int? packageId);
        Task<List<YearPackage>> GetYearPackagesBySchoolId(int schoolId);
        Task<YearPackage> CreateYearPackage(YearPackage entity);
        Task<YearPackage> GetValidYearPackageBySchoolYearId(int schoolYearId);
        Task<List<YearPackage>> GetListValidYearPackageBySchoolYearId(int schoolYearId);
        Task<YearPackage> GetValidYearPackageBySchoolYearIdAndPackageId(int schoolYearId, int packageId);
    }
}
