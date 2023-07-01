using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMapper _mapper;
        private readonly FileService _fileService;

        public ProjectService(IProjectRepository projectRepository, ICollaboratorRepository collaboratorRepository,
            IMapper mapper, FileService fileService)
        {
            _projectRepository = projectRepository;
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public IEnumerable<ProjectDto> GetAll() =>
            _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(_projectRepository.GetAll());

        public ProjectDto Get(Func<Project, bool> predicate) =>
            _mapper.Map<Project, ProjectDto>(_projectRepository.Get(predicate));

        public bool Exist(Func<Project, bool> predicate) => _projectRepository.Exist(predicate);

        public async void Add(ProjectDto projectDto)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                CustomerCompanyName = projectDto.CustomerCompanyName,
                PerformerCompanyName = projectDto.PerformerCompanyName,
                DirectorId = projectDto.DirectorId,
                Priority = projectDto.Priority,
                ProjectStartDate = projectDto.ProjectStartDate.ConvertToDateOnly(),
                ProjectEndDate = projectDto.ProjectEndDate.ConvertToDateOnly(),
            };

            var collaborators = projectDto.Collaborators?
                .Select(collaboratorDto => _collaboratorRepository.Get(collaborator => collaborator.Id == collaboratorDto.Id))
                .ToList();

            project.Collaborators = collaborators;


            if (projectDto.Files != null)
            {
                var filesUrl = new List<string>();
                foreach (var formFile in projectDto.Files)
                {
                    string url = await _fileService.Upload(nameof(project.Name), formFile);
                    filesUrl.Add(url);
                }

                project.FilesUrl = filesUrl;
            }

            _projectRepository.Create(project);
        }

        public void Update(ProjectDto projectDto)
        {
            var project = _projectRepository.Get(project => project.Id == projectDto.Id);

            project.Collaborators?.Clear();
            project.Collaborators = projectDto.Collaborators?
                .Select(collaboratorDto => _collaboratorRepository.Get(collaborator => collaborator.Id == collaboratorDto.Id))
                .ToList();

            project.Name = projectDto.Name;
            project.CustomerCompanyName = projectDto.CustomerCompanyName;
            project.PerformerCompanyName = projectDto.PerformerCompanyName;
            project.DirectorId = projectDto.DirectorId;
            project.Priority = projectDto.Priority;
            project.ProjectStartDate = projectDto.ProjectStartDate.ConvertToDateOnly();
            project.ProjectEndDate = projectDto.ProjectEndDate.ConvertToDateOnly();

            _projectRepository.Update(project);
        }

        public void Delete(ProjectDto projectDto)
        {
            var project = _projectRepository.Get(project => project.Id == projectDto.Id);
            _projectRepository.Delete(project);
        }
    }
}