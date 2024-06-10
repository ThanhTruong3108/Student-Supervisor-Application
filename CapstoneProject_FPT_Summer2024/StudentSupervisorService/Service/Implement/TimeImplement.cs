using AutoMapper;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.TimeRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.TimeResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class TimeImplement : TimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TimeImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfTime>> CreateTime(RequestOfTime request)
        {
            var response = new DataResponse<ResponseOfTime>();

            try
            {
                var createTime = _mapper.Map<Time>(request);
                _unitOfWork.Time.Add(createTime);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfTime>(createTime);
                response.Message = "Create Successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task DeleteTime(int timeId)
        {
            var time = _unitOfWork.Time.GetById(timeId);
            if (time is null)
            {
                throw new Exception("Can not found by" + timeId);
            }
            _unitOfWork.Time.Remove(time);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfTime>>> GetAllTimes()
        {
            var response = new DataResponse<List<ResponseOfTime>>();

            try
            {
                var times = await _unitOfWork.Time.GetAllTimes();
                if (times is null || !times.Any())
                {
                    response.Message = "The Time list is empty";
                    response.Success = true;
                    return response;
                }
                response.Data = _mapper.Map<List<ResponseOfTime>>(times);
                response.Message = "List Times";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfTime>> GetTimeById(int id)
        {
            var response = new DataResponse<ResponseOfTime>();

            try
            {
                var time = await _unitOfWork.Time.GetTimeById(id);
                if (time is null)
                {
                    throw new Exception("The Time does not exist");
                }
                response.Data = _mapper.Map<ResponseOfTime>(time);
                response.Message = $"TimeId {time.TimeId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfTime>>> SearchTimes(byte? slot, TimeSpan? time, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfTime>>();

            try
            {
                var times = await _unitOfWork.Time.SearchTimes(slot, time);
                if (times is null || times.Count == 0)
                {
                    response.Message = "No Times found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var timeDTO = _mapper.Map<List<ResponseOfTime>>(times);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        timeDTO = timeDTO.OrderByDescending(p => p.Slot).ToList();
                    }
                    else
                    {
                        timeDTO = timeDTO.OrderBy(p => p.Slot).ToList();
                    }

                    response.Data = timeDTO;
                    response.Message = "Times found";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfTime>> UpdateTime(int id, RequestOfTime request)
        {
            var response = new DataResponse<ResponseOfTime>();

            try
            {
                var time = _unitOfWork.Time.GetById(id);
                if (time is null)
                {
                    response.Message = "Can not found Time";
                    response.Success = false;
                    return response;
                }
                time.ClassGroupId = request.ClassGroupId;
                time.Slot = request.Slot;
                time.Time1 = request.Time1;
                _unitOfWork.Time.Update(time);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfTime>(time);
                response.Success = true;
                response.Message = "Update Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
