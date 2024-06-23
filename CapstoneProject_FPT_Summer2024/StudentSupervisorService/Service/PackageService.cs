using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PackageResponse;
using StudentSupervisorService.Models.Request.PackageRequest;

namespace StudentSupervisorService.Service
{
    public interface PackageService
    {
        Task<DataResponse<List<ResponseOfPackage>>> GetAllPackages(string sortOrder);
        Task<DataResponse<ResponseOfPackage>> GetPackageById(int id);
        Task<DataResponse<ResponseOfPackage>> CreatePackage(PackageRequest request);
        Task DeletePackage(int id);
        Task<DataResponse<ResponseOfPackage>> UpdatePackage(int id, PackageRequest request);
        Task<DataResponse<List<ResponseOfPackage>>> SearchPackages(string? name, int? minPrice, int? maxPrice, string? type, string sortOrder);
    }
}
