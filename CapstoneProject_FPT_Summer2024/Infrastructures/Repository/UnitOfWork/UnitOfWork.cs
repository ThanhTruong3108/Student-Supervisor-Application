using Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using StudentSupervisorService.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly SchoolRulesContext _context;






        public void RollBack(IDbContextTransaction commit, string name)
        {
            commit.RollbackToSavepoint(name);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public IDbContextTransaction StartTransaction(string name)
        {
            var commit = _context.Database.BeginTransaction();
            commit.CreateSavepoint(name);
            return commit;
        }

        public void StopTransaction(IDbContextTransaction commit)
        {
            commit.Commit();
        }
    }
}
