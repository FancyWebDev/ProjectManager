using AutoMapper;
using BLL.DTO;
using DAL.Models;
using Task = DAL.Models.Task;

namespace BLL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
        CreateMap<Collaborator, CollaboratorDto>();
        CreateMap<CollaboratorDto, Collaborator>();
        CreateMap<Task, TaskDto>();
        CreateMap<TaskDto, Task>();
    }
}