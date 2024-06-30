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
    public class PenaltyRepository : GenericRepository<Penalty>, IPenaltyRepository
    {
        public PenaltyRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Penalty>> GetAllPenalties()
        {
            return await _context.Penalties.ToListAsync();
        }

        public async Task<Penalty> GetPenaltyById(int id)
        {
            return await _context.Penalties.FirstOrDefaultAsync(x => x.PenaltyId == id);
        }

        public async Task<List<Penalty>> SearchPenalties(int? schoolId, string? name, string? description)
        {
            var query = _context.Penalties.AsQueryable();

            if (schoolId != null)
            {
                query = query.Where(p => p.SchoolId == schoolId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }

            return await query.ToListAsync();
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
            
        }   
    }
}
