using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using Infrastructures.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using StudentSupervisorService.Models.Request.StudentRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.StudentResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class StudentImplement : StudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DataResponse<List<StudentResponse>>> GetAllStudents(string sortOrder)
        {
            var response = new DataResponse<List<StudentResponse>>();
            try
            {
                var studentEntities = await unitOfWork.Student.GetAllStudents();
                if (studentEntities is null || !studentEntities.Any())
                {
                    response.Message = "The Student list is empty";
                    response.Success = true;
                    return response;
                }

                studentEntities = sortOrder == "desc"
                    ? studentEntities.OrderByDescending(r => r.Code).ToList()
                    : studentEntities.OrderBy(r => r.Code).ToList();

                response.Data = mapper.Map<List<StudentResponse>>(studentEntities);
                response.Message = "List Students";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<StudentResponse>> GetStudentById(int id)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var studentEntity = await unitOfWork.Student.GetStudentById(id);
                if (studentEntity == null)
                {
                    response.Message = "Student not found";
                    response.Success = false;
                    return response;
                }

                response.Data = mapper.Map<StudentResponse>(studentEntity);
                response.Message = "Found a Student";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<StudentResponse>>> SearchStudents(int? schoolId, string? code, string? name, bool? sex, DateTime? birthday, string? address, string? phone, string sortOrder)
        {
            var response = new DataResponse<List<StudentResponse>>();

            try
            {
                var studentEntities = await unitOfWork.Student.SearchStudents(schoolId, code, name, sex, birthday, address, phone);
                if (studentEntities is null || studentEntities.Count == 0)
                {
                    response.Message = "No Student matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        studentEntities = studentEntities.OrderByDescending(r => r.Code).ToList();
                    }
                    else
                    {
                        studentEntities = studentEntities.OrderBy(r => r.Code).ToList();
                    }
                    response.Data = mapper.Map<List<StudentResponse>>(studentEntities);
                    response.Message = "List Students";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<StudentResponse>> CreateStudent(StudentCreateRequest request)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var studentEntity = new Student
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.Name,
                    Sex = request.Sex,
                    Birthday = request.Birthday,
                    Address = request.Address,
                    Phone = request.Phone
                };

                var created = await unitOfWork.Student.CreateStudent(studentEntity);

                response.Data = mapper.Map<StudentResponse>(created);
                response.Message = "Student created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Student Class failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<StudentResponse>> UpdateStudent(StudentUpdateRequest request)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var existingStudent = await unitOfWork.Student.GetStudentById(request.StudentId);
                if (existingStudent == null)
                {
                    response.Data = "Empty";
                    response.Message = "Student not found";
                    response.Success = false;
                    return response;
                }

                existingStudent.SchoolId = request.SchoolId ?? existingStudent.SchoolId;
                existingStudent.Code = request.Code ?? existingStudent.Code;
                existingStudent.Name = request.Name ?? existingStudent.Name;
                existingStudent.Sex = request.Sex ?? existingStudent.Sex;
                existingStudent.Birthday = request.Birthday ?? existingStudent.Birthday;
                existingStudent.Address = request.Address ?? existingStudent.Address;
                existingStudent.Phone = request.Phone ?? existingStudent.Phone;

                await unitOfWork.Student.UpdateStudent(existingStudent);

                response.Data = mapper.Map<StudentResponse>(existingStudent);
                response.Message = "Student updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update Student failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
