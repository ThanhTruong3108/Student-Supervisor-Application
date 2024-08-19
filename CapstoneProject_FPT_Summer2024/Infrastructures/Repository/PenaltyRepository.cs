using Domain.Entity;
using Domain.Enums.Status;
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
    public class PenaltyRepository : GenericRepository<Penalty>, IPenaltyRepository
    {
        public PenaltyRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Penalty>> GetAllPenalties()
        {
            return await _context.Penalties
                .Include(c => c.School)
                .ToListAsync();
        }

        public async Task<Penalty> GetPenaltyById(int id)
        {
            return await _context.Penalties
                .Include(c => c.School)
                .FirstOrDefaultAsync(x => x.PenaltyId == id);
        }

        public async Task<Penalty> CreatePenalty(Penalty penaltyEntity)
        {
            await _context.Penalties.AddAsync(penaltyEntity);
            await _context.SaveChangesAsync();
            return penaltyEntity;
        }

        public async Task<Penalty> UpdatePenalty(Penalty penaltyEntity)
        {
            _context.Penalties.Update(penaltyEntity);
            await _context.SaveChangesAsync();
            return penaltyEntity;
        }

        public async Task DeletePenalty(int id)
        {
            var penaltyEntity = await _context.Penalties.FindAsync(id);
            penaltyEntity.Status = PenaltyStatusEnums.INACTIVE.ToString();
            _context.Entry(penaltyEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Penalty>> GetPenaltiesBySchoolId(int schoolId)
        {
            return await _context.Penalties
                .Include(c => c.School)
                .Where(u => u.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<List<Penalty>> GetActivePenaltiesBySchoolId(int schoolId)
        {
            return await _context.Penalties
                .Include(c => c.School)
                .Where(u => u.SchoolId == schoolId && u.Status == PenaltyStatusEnums.ACTIVE.ToString())
                .ToListAsync();
        }
    }
}
