using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ISemesterRepository : IGenericRepository<Semester>
    {
        Task<List<Semester>> GetSemestersBySchoolId(int schoolId);
    }
}
