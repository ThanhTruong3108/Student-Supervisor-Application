using AutoMapper;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class ClassImplement : ClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<ClassResponse>>> GetAllClasses(int page, int pageSize, string sortOrder)
        {
            var response = new DataResponse<List<ClassResponse>>();
            try
            {
                var classEntities = await _unitOfWork.Class.GetAllClasses();
                if (classEntities is null || !classEntities.Any())
                {
                    response.Message = "The Class list is empty";
                    response.Success = true;
                    return response;
                }

                var pagedClasses = classEntities.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                pagedClasses = sortOrder == "desc"
                    ? pagedClasses.OrderByDescending(r => r.Code).ToList()
                    : pagedClasses.OrderBy(r => r.Code).ToList();                

                response.Data = _mapper.Map<List<ClassResponse>>(pagedClasses);
                response.Message = "List Classes";
                response.Success = true; 
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> GetClassById(int id)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var classEntity = await _unitOfWork.Class.GetClassById(id);
                if (classEntity == null)
                {
                    response.Message = "Class not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ClassResponse>(classEntity);
                response.Message = "Found a Class";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ClassResponse>>> SearchClasses(int? schoolYearId, int? classGroupId, string? code, string? room, string? name, int? totalPoint, string sortOrder)
        {
            var response = new DataResponse<List<ClassResponse>>();

            try
            {
                var classEntities = await _unitOfWork.Class.SearchClasses(schoolYearId, classGroupId, code, room, name, totalPoint);
                if (classEntities is null || classEntities.Count == 0)
                {
                    response.Message = "No Class matches the search criteria";
                    response.Success = true;
                } else
                {
                    if (sortOrder == "desc")
                    {
                        classEntities = classEntities.OrderByDescending(r => r.Code).ToList();
                    } else
                    {
                        classEntities = classEntities.OrderBy(r => r.Code).ToList();
                    }
                    response.Data = _mapper.Map<List<ClassResponse>>(classEntities);
                    response.Message = "List Classes";
                    response.Success = true;
                }
            } catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ClassResponse>> CreateClass(ClassCreateRequest request)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var classEntity = new Class
                {
                    SchoolYearId = request.SchoolYearId,
                    ClassGroupId = request.ClassGroupId,
                    Code = request.Code,
                    Room = request.Room,
                    Name = request.Name,
                    TotalPoint = request.TotalPoint
                };

                var created = await _unitOfWork.Class.CreateClass(classEntity);

                response.Data = _mapper.Map<ClassResponse>(created);
                response.Message = "Class created successfully";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Create Class failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> UpdateClass(ClassUpdateRequest request)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var existingClass = await _unitOfWork.Class.GetClassById(request.ClassId);
                if (existingClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "Class not found";
                    response.Success = false;
                    return response;
                }

                existingClass.SchoolYearId = request.SchoolYearId ?? existingClass.SchoolYearId;
                existingClass.ClassGroupId = request.ClassGroupId ?? existingClass.ClassGroupId;
                existingClass.Code = request.Code ?? existingClass.Code;
                existingClass.Room = request.Room ?? existingClass.Room;
                existingClass.Name = request.Name ?? existingClass.Name;
                existingClass.TotalPoint = request.TotalPoint ?? existingClass.TotalPoint;

                await _unitOfWork.Class.UpdateClass(existingClass);

                response.Data = _mapper.Map<ClassResponse>(existingClass);
                response.Message = "Class updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update Class failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> DeleteClass(int id)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var existingClass = await _unitOfWork.Class.GetClassById(id);
                if (existingClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "Class not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.Class.DeleteClass(id);
                response.Data = "Empty";
                response.Message = "Class deleted successfully";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
