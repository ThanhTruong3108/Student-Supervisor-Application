using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IPatrolScheduleRepository : IGenericRepository<PatrolSchedule>
    {
        Task<List<PatrolSchedule>> GetAllPatrolSchedules();
        Task<PatrolSchedule> GetPatrolScheduleById(int id);
        Task<PatrolSchedule> CreatePatrolSchedule(PatrolSchedule patrolScheduleEntity);
        Task<PatrolSchedule> UpdatePatrolSchedule(PatrolSchedule patrolScheduleEntity);
        Task DeletePatrolSchedule(int id);
        Task<List<PatrolSchedule>> GetPatrolSchedulesBySchoolId(int schoolId);
        Task<List<PatrolSchedule>> GetPatrolSchedulesByUserId(int userId);
        Task<List<PatrolSchedule>> GetPatrolSchedulesBySupervisorUserId(int userId);
        Task<List<PatrolSchedule>> GetOngoingPatrolSchedulesOver1Day();
        Task UpdateMultiplePatrolSchedules(List<PatrolSchedule> patrolSchedules);
    }
}
