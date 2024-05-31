
namespace StudentSupervisorService.Models.Response.ClassResponse
{
    public class ClassResponse
    {
        public int ClassId { get; set; }
        public int SchoolYearId { get; set; }
        public int ClassGroupId { get; set; }
        public string? Code { get; set; }
        public string? Room { get; set; }
        public string? Name { get; set; }
        public int? TotalPoint { get; set; }
    }
}
