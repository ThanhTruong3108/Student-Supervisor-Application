using Domain.Entity;
using Domain.Entity.DTO;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IEvaluationRepository : IGenericRepository<Evaluation>
    {
        Task<List<Evaluation>> GetAllEvaluations();
        Task<Evaluation> GetEvaluationById(int id);
        Task<Evaluation> CreateEvaluation(Evaluation evaluationEntity);
        Task<Evaluation> UpdateEvaluation(Evaluation evaluationEntity);
        Task<List<Evaluation>> GetEvaluationsByClassIdAndDateRange(int classId, DateTime from, DateTime to);
        Task<List<Evaluation>> GetEvaluationsBySchoolId(int schoolId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<List<EvaluationRanking>> GetEvaluationRankings(int schoolId, short year, string? semesterName = null, int? month = null, int? week = null);
    }
}
