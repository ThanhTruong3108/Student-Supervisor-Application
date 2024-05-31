using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class TimeRepository : GenericRepository<Time>, ITimeRepository
    {
        public TimeRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Time>> GetAllTimes()
        {
            var times = await _context.Times
                .Include(c => c.ClassGroup)
                .ToListAsync();
            return times;
        }

        public async Task<Time> GetTimeById(int id)
        {
            return _context.Times
               .Include(c => c.ClassGroup)
               .FirstOrDefault(s => s.TimeId == id);
        }

        public async Task<List<Time>> SearchTimes(byte? slot, TimeSpan? time)
        {
            var query = _context.Times.AsQueryable();

            if (slot.HasValue)
            {
                query = query.Where(p => p.Slot == slot.Value);
            }

            if (time.HasValue)
            {
                query = query.Where(p => p.Time1 >= time.Value);
            }

            return await query.Include(c => c.ClassGroup).ToListAsync();
        }
    }
}
