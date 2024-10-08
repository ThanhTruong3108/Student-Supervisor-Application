﻿using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface ISchoolYearRepository : IGenericRepository<SchoolYear>
    {
        Task<List<SchoolYear>> GetAllSchoolYears();
        Task<SchoolYear> GetSchoolYearById(int id);
        Task<SchoolYear> GetSchoolYearBySchoolIdAndYear(int schoolId, int year);
        Task<SchoolYear> GetOngoingSchoolYearBySchoolIdAndYear(int schoolId, int year);
        Task<List<SchoolYear>> GetOngoingSchoolYearsOver1Day();
        Task UpdateMultipleSchoolYears(List<SchoolYear> schoolYears);
        Task<List<SchoolYear>> GetSchoolYearBySchoolId(int schoolId);
        Task<SchoolYear> GetYearBySchoolYearId(int schoolId, int year);
    }
}
