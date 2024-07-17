using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PenaltyRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PenaltyResponse;

namespace StudentSupervisorService.Service.Implement
{
    public class PenaltyImplement : PenaltyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PenaltyImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<PenaltyResponse>>> GetAllPenalties(string sortOrder)
        {
            var response = new DataResponse<List<PenaltyResponse>>();
            try
            {
                var penaltyEntities = await _unitOfWork.Penalty.GetAllPenalties();
                if (penaltyEntities is null || !penaltyEntities.Any())
                {
                    response.Message = "The Penalty list is empty";
                    response.Success = true;
                    return response;
                }

                penaltyEntities = sortOrder == "desc"
                    ? penaltyEntities.OrderByDescending(r => r.PenaltyId).ToList()
                    : penaltyEntities.OrderBy(r => r.PenaltyId).ToList();

                response.Data = _mapper.Map<List<PenaltyResponse>>(penaltyEntities);
                response.Message = "List Penalty";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<PenaltyResponse>> GetPenaltyById(int id)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var penaltyEntity = await _unitOfWork.Penalty.GetPenaltyById(id);
                if (penaltyEntity == null)
                {
                    response.Message = "Penalty not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<PenaltyResponse>(penaltyEntity);
                response.Message = "Found a Penalty";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<PenaltyResponse>>> SearchPenalties(int? schoolId, string? name, string? description, string? status, string sortOrder)
        {
            var response = new DataResponse<List<PenaltyResponse>>();

            try
            {
                var penaltyEntities = await _unitOfWork.Penalty.SearchPenalties(schoolId, name, description, status);
                if (penaltyEntities is null || penaltyEntities.Count == 0)
                {
                    response.Message = "No Penalty matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        penaltyEntities = penaltyEntities.OrderByDescending(r => r.PenaltyId).ToList();
                    }
                    else
                    {
                        penaltyEntities = penaltyEntities.OrderBy(r => r.PenaltyId).ToList();
                    }
                    response.Data = _mapper.Map<List<PenaltyResponse>>(penaltyEntities);
                    response.Message = "List Penalty";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<PenaltyResponse>> CreatePenalty(PenaltyCreateRequest penaltyCreateRequest)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var isExistCode = _unitOfWork.Penalty.Find(s => s.Code == penaltyCreateRequest.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Code already in use!";
                    response.Success = false;
                    return response;
                }

                var penaltyEntity = new Penalty
                {
                    SchoolId = penaltyCreateRequest.SchoolId,
                    Code = penaltyCreateRequest.Code,
                    Name = penaltyCreateRequest.Name,
                    Description = penaltyCreateRequest.Description,
                    Status = PenaltyStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.Penalty.CreatePenalty(penaltyEntity);

                response.Data = _mapper.Map<PenaltyResponse>(created);
                response.Message = "Penalty created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create Penalty failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<PenaltyResponse>> UpdatePenalty(PenaltyUpdateRequest penaltyUpdateRequest)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var existingPenalty = await _unitOfWork.Penalty.GetPenaltyById(penaltyUpdateRequest.PenaltyId);
                if (existingPenalty == null)
                {
                    response.Data = "Empty";
                    response.Message = "Penalty not found";
                    response.Success = false;
                    return response;
                }

                var isExistCode = _unitOfWork.Penalty.Find(s => s.Code == penaltyUpdateRequest.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Code already in use!";
                    response.Success = false;
                    return response;
                }

                existingPenalty.SchoolId = penaltyUpdateRequest.SchoolId ?? existingPenalty.SchoolId;
                existingPenalty.Code = penaltyUpdateRequest.Code ?? existingPenalty.Code;
                existingPenalty.Name = penaltyUpdateRequest.Name ?? existingPenalty.Name;
                existingPenalty.Description = penaltyUpdateRequest.Description ?? existingPenalty.Description;

                await _unitOfWork.Penalty.UpdatePenalty(existingPenalty);

                response.Data = _mapper.Map<PenaltyResponse>(existingPenalty);
                response.Message = "Penalty updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update Penalty failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<PenaltyResponse>> DeletePenalty(int id)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var existingPenalty = await _unitOfWork.Penalty.GetPenaltyById(id);
                if (existingPenalty == null)
                {
                    response.Data = "Empty";
                    response.Message = "Penalty not found";
                    response.Success = false;
                    return response;
                }

                if (existingPenalty.Status == PenaltyStatusEnums.INACTIVE.ToString())
                {
                    response.Data = null;
                    response.Message = "Penalty is already deleted";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.Penalty.DeletePenalty(id);
                response.Data = "Empty";
                response.Message = "Penalty deleted successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<PenaltyResponse>>> GetPenaltiesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<PenaltyResponse>>();

            try
            {
                var penalties = await _unitOfWork.Penalty.GetPenaltiesBySchoolId(schoolId);
                if (penalties == null || !penalties.Any())
                {
                    response.Message = "No Penaltys found for the specified SchoolId.";
                    response.Success = false;
                }
                else
                {
                    var penaltyDTOs = _mapper.Map<List<PenaltyResponse>>(penalties);
                    response.Data = penaltyDTOs;
                    response.Message = "Penaltys found";
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
    }
}
