﻿using Domain.Entity;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IClassGroupRepository : IGenericRepository<ClassGroup>
    {
        Task<List<ClassGroup>> GetAllClassGroups();
        Task<ClassGroup> GetClassGroupById(int id);
        Task<ClassGroup> CreateClassGroup(ClassGroup classGroupEntity);
        Task<ClassGroup> UpdateClassGroup(ClassGroup classGroupEntity);
        Task DeleteClassGroup(int id);
        Task<List<ClassGroup>> GetClassGroupsBySchoolId(int schoolId);
        Task<List<ClassGroup>> GetClassGroupsByUserId(int userId);
    }
}
