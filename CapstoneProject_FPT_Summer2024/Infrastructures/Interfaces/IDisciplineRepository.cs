using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IDisciplineRepository : IGenericRepository<Discipline>
    {
        Task<List<Discipline>> GetAllDisciplines();
        Task<Discipline> GetDisciplineById(int id);
        Task<Discipline> CreateDiscipline(Discipline disciplineEntity);
        Task<Discipline> UpdateDiscipline(Discipline disciplineEntity);
        Task DeleteDiscipline(int id);
        Task<List<Discipline>> GetDisciplinesBySchoolId(int schoolId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<Discipline> GetDisciplineByViolationId(int violationId);
        Task<List<Discipline>> GetDisciplinesByUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<List<Discipline>> GetDisciplinesBySupervisorUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
    }
}
