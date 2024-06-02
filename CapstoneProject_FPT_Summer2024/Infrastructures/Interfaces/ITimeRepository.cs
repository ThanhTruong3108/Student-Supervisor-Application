using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ITimeRepository : IGenericRepository<Time>
    {
        Task<List<Time>> GetAllTimes();
        Task<Time> GetTimeById(int id);
        Task<List<Time>> SearchTimes(byte? slot, TimeSpan? time);
    }
}
