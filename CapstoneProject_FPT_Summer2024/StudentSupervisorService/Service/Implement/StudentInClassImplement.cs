using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.StudentInClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.StudentInClassResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class StudentInClassImplement : StudentInClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentInClassImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<StudentInClassResponse>>> GetAllStudentInClass(string sortOrder)
        {
            var response = new DataResponse<List<StudentInClassResponse>>();
            try
            {

                var studentInClassEntities = await _unitOfWork.StudentInClass.GetAllStudentInClass();
                if (studentInClassEntities is null || !studentInClassEntities.Any())
                {
                    response.Message = "The StudentInClass list is empty";
                    response.Success = true;
                    return response;
                }

                studentInClassEntities = sortOrder == "desc"
                    ? studentInClassEntities.OrderByDescending(r => r.StudentInClassId).ToList()
                    : studentInClassEntities.OrderBy(r => r.StudentInClassId).ToList();

                response.Data = _mapper.Map<List<StudentInClassResponse>>(studentInClassEntities);
                response.Message = "List StudentInClass";
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

        public async Task<DataResponse<StudentInClassResponse>> GetStudentInClassById(int id)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var studentInClassEntity = await _unitOfWork.StudentInClass.GetStudentInClassById(id);
                if (studentInClassEntity == null)
                {
                    response.Message = "StudentInClass not found";
                    response.Success = false;
                    return response;
                }
                response.Data = _mapper.Map<StudentInClassResponse>(studentInClassEntity);
                response.Message = "Found a StudentInClass";
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

        public async Task<DataResponse<List<StudentInClassResponse>>> SearchStudentInClass(int? classId, int? studentId, DateTime? enrollDate, bool? isSupervisor, string? status, string sortOrder)
        {
            var response = new DataResponse<List<StudentInClassResponse>>();

            try
            {
                var studentInClassEntities = await _unitOfWork.StudentInClass.SearchStudentInClass(classId, studentId, enrollDate, isSupervisor, status);
                if (studentInClassEntities is null || studentInClassEntities.Count == 0)
                {
                    response.Message = "No StudentInClass matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        studentInClassEntities = studentInClassEntities.OrderByDescending(r => r.StudentInClassId).ToList();
                    }
                    else
                    {
                        studentInClassEntities = studentInClassEntities.OrderBy(r => r.StudentInClassId).ToList();
                    }
                    response.Data = _mapper.Map<List<StudentInClassResponse>>(studentInClassEntities);
                    response.Message = "List StudentInClass";
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

        public async Task<DataResponse<StudentInClassResponse>> CreateStudentInClass(StudentInClassCreateRequest request)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var studentInClassEntity = new StudentInClass
                {
                    ClassId = request.ClassId,
                    StudentId = request.StudentId,
                    EnrollDate = request.EnrollDate,
                    IsSupervisor = request.IsSupervisor,
                    Status = StudentInClassStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.StudentInClass.CreateStudentInClass(studentInClassEntity);

                response.Data = _mapper.Map<StudentInClassResponse>(created);
                response.Message = "StudentInClass created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create StudentInClass failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
        public async Task<DataResponse<StudentInClassResponse>> UpdateStudentInClass(StudentInClassUpdateRequest request)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var existingStudentInClass = await _unitOfWork.StudentInClass.GetStudentInClassById(request.StudentInClassId);
                if (existingStudentInClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "StudentInClass not found";
                    response.Success = false;
                    return response;
                }

                existingStudentInClass.ClassId = request.ClassId ?? existingStudentInClass.ClassId;
                existingStudentInClass.StudentId = request.StudentId ?? existingStudentInClass.StudentId;
                existingStudentInClass.EnrollDate = request.EnrollDate ?? existingStudentInClass.EnrollDate;
                existingStudentInClass.IsSupervisor = request.IsSupervisor ?? existingStudentInClass.IsSupervisor;
                existingStudentInClass.Status = request.Status ?? existingStudentInClass.Status;

                await _unitOfWork.StudentInClass.UpdateStudentInClass(existingStudentInClass);

                response.Data = _mapper.Map<StudentInClassResponse>(existingStudentInClass);
                response.Message = "StudentInClass updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update StudentInClass failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<StudentInClassResponse>> DeleteStudentInClass(int id)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var existingStudentInClass = await _unitOfWork.StudentInClass.GetStudentInClassById(id);
                if (existingStudentInClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "StudentInClass not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.StudentInClass.DeleteStudentInClass(id);
                response.Data = "Empty";
                response.Message = "StudentInClass deleted successfully";
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
    }
}
