using Coravel.Invocable;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class WeeklyScheduleImplement : IInvocable
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeeklyScheduleImplement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke()
        {
            var createWeeklyEvaluationAtSundayResult = await CreateWeeklyEvaluationAtSunday();
            await Console.Out.WriteLineAsync(createWeeklyEvaluationAtSundayResult ? "CreateWeeklyEvaluationAtSunday success" : "CreateWeeklyEvaluationAtSunday failed");
        }

        // Tạo thống kê Evaluation hàng tuần vào chủ nhật
        private async Task<bool> CreateWeeklyEvaluationAtSunday()
        {
            bool result = false;
            try
            {
                // lấy tất cả các lớp đang ACTIVE
                var activeClasses = await _unitOfWork.Class.GetAllActiveClasses();
                if (activeClasses != null)
                {
                    foreach (var activeClass in activeClasses)
                    {
                        // Tạo Evaluation hàng tuần theo từng lớp
                        var weeklyEvaluation = new Evaluation
                        {
                            ClassId = activeClass.ClassId,
                            From = DateTime.Now.AddDays(-6),
                            To = DateTime.Now,
                            Description = $"Thống kê hàng tuần của lớp {activeClass.Name} từ {DateTime.Now.AddDays(-6):dd/MM/yyyy} đến {DateTime.Now:dd/MM/yyyy}",
                            Points = activeClass.TotalPoint,
                            Status = EvaluationStatusEnums.ACTIVE.ToString()
                        };
                        // Nếu tạo Evaluation thành công thì reset lại điểm của lớp
                        if (await _unitOfWork.Evaluation.CreateEvaluation(weeklyEvaluation) != null)
                        {
                            activeClass.TotalPoint = 100;
                            _unitOfWork.Class.Update(activeClass);
                            _unitOfWork.Save();
                            result = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in CreateWeeklyEvaluationAtSunday: " + e.Message);
                return false;
            }
            return result;
        }
    }
}
