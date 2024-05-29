using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IHighSchoolRepository HighSchool { get; }
        ISchoolYearRepository SchoolYear { get; }
        IDbContextTransaction StartTransaction(string name);
        void StopTransaction(IDbContextTransaction commit);
        void RollBack(IDbContextTransaction commit, string name);
        int Save();
    }
}
