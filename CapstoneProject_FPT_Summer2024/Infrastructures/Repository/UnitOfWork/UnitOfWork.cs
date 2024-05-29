using Azure.Core;
using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly SchoolRulesContext _context;
        public UnitOfWork(SchoolRulesContext context)
        {
            _context = context;
            HighSchool = new HighSchoolRepository(_context);
            SchoolYear = new SchoolYearRepository(_context);
        }
        public SchoolRulesContext Context { get { return _context; } }
        public IHighSchoolRepository HighSchool { get; }
        public ISchoolYearRepository SchoolYear { get; }

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
