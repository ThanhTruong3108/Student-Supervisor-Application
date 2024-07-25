using AutoMapper;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentSupervisorService.Service.Implement
{
    public class ValidationImplement : ValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidationImplement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ktra xem có Package nào VALID trong SchoolYear đang ONGOING ko
        public async Task<bool> IsAnyValidPackageInSpecificYear(int schoolId, int year)
        {
            bool result = false;
            try
            {
                // lấy SchoolYear đang ONGOING bằng schoolId và year của Violation
                var matchedSchoolYear = await _unitOfWork.SchoolYear.GetOngoingSchoolYearBySchoolIdAndYear(schoolId, year);
                if (matchedSchoolYear is null)
                {
                    await Console.Out.WriteLineAsync("Ko có SchoolYear nào đang ONGOING");
                    return result;
                }
                // lấy YearPackage đang VALID bằng SchoolYearId
                var existedYearPackage = await _unitOfWork.YearPackage.GetValidYearPackageBySchoolYearId(matchedSchoolYear.SchoolYearId);
                if (existedYearPackage is null)
                {
                    await Console.Out.WriteLineAsync("Ko có YearPackage nào đang VALID");
                    return result;
                }
                result = true;
            } catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            return result;
        }
    }
}
