using AutoMapper;
using Azure;
using Azure.Core;
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

        // kiểm tra HighSchool trùng lặp theo Code và Name
        public async Task<bool> IsHighSchoolDuplicated(int registeredId, string? code, string? name)
        {
            bool result = false;
            try
            {
                var existedRegisterSchool = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(registeredId);
                if (!existedRegisterSchool.School.Code.Equals(code) || !existedRegisterSchool.School.Name.Equals(name))
                {
                    var existedSchool = await _unitOfWork.HighSchool.GetHighSchoolByCodeOrName(code, name);
                    if (existedSchool != null)
                    {
                        result = true;
                        await Console.Out.WriteLineAsync("Code hoặc Name của HighSchool đã tồn tại");
                    }
                }
            } catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            return result;
        }

        public async Task<bool> IsViolationCreatedOver1HourFromTimeInPatrolSchedule(int patrolScheduleId)
        {
            bool result = false;
            try
            {
                var existedPatrolSchedule = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(patrolScheduleId);
                if (existedPatrolSchedule is null)
                {
                    await Console.Out.WriteLineAsync("Không tồn tại PatrolSchedule");
                    return result;
                }
                // lấy Time của PatrolSchedule + 1h
                var timeAdd1Hour = existedPatrolSchedule.Time.Value.Add(TimeSpan.FromHours(1));
                if (DateTime.Now.TimeOfDay > timeAdd1Hour)
                {
                    result = true;
                    return result;
                } else
                {
                    result = false;
                    return result;
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            return result;
        }
    }
}
