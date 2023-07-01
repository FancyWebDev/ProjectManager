using System;
using System.Collections.Generic;
using BLL.DTO;
using DAL.Models;

namespace BLL.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAll();
        ProjectDto Get(Func<Project, bool> predicate);
        bool Exist(Func<Project, bool> predicate);
        void Add(ProjectDto projectDto);
        void Update(ProjectDto projectDto);
        void Delete(ProjectDto projectDto);
    }
}