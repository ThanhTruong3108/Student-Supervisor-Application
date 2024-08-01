using Coravel.Invocable;
using Infrastructures.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class ScheduleImplement : IInvocable
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleImplement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Invoke()
        {
            await Console.Out.WriteLineAsync("This is cronjob");
        }
    }
}
