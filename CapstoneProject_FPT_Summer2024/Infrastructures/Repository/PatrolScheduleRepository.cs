﻿using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repository
{
    public class PatrolScheduleRepository : GenericRepository<PatrolSchedule>, IPatrolScheduleRepository
    {
        public PatrolScheduleRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<PatrolSchedule>> GetAllPatrolSchedules()
        {
            return await _context.PatrolSchedules
                .Include(v => v.Class)
                .Include(p => p.Supervisor)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<PatrolSchedule> GetPatrolScheduleById(int id)
        {
            return await _context.PatrolSchedules
                .Include(v => v.Class)
                .Include(p => p.Supervisor)
                .Include(p => p.User)
                .FirstOrDefaultAsync(x => x.ScheduleId == id);
        }

        public async Task<PatrolSchedule> CreatePatrolSchedule(PatrolSchedule patrolScheduleEntity)
        {
            await _context.PatrolSchedules.AddAsync(patrolScheduleEntity);
            await _context.SaveChangesAsync();
            return patrolScheduleEntity;
        }

        public async Task<PatrolSchedule> UpdatePatrolSchedule(PatrolSchedule patrolScheduleEntity)
        {
            _context.PatrolSchedules.Update(patrolScheduleEntity);
            _context.Entry(patrolScheduleEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return patrolScheduleEntity;
        }

        public async Task DeletePatrolSchedule(int id)
        {
            try
            {
                var pScheduleEntity = await _context.PatrolSchedules.FindAsync(id);
                pScheduleEntity.Status = PatrolScheduleStatusEnums.INACTIVE.ToString();
                _context.Entry(pScheduleEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + (ex.InnerException != null ? ex.InnerException.Message : ""));
            }
        }

        public async Task<List<PatrolSchedule>> GetPatrolSchedulesBySchoolId(int schoolId)
        {
            return await _context.PatrolSchedules
                .Include(v => v.Class)
                .Include(p => p.Supervisor)
                .Include(p => p.User)
                .Where(v => v.User.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<List<PatrolSchedule>> GetPatrolSchedulesByStudentSupervisorId(int studentSupervisorId)
        {
            return await _context.PatrolSchedules
                .Include(v => v.Class)
                .Include(p => p.Supervisor)
                .Include(p => p.User)
                .Where(p => p.Supervisor.StudentSupervisorId == studentSupervisorId)
                .ToListAsync();
        }
    }
}
