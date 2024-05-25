using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;


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
