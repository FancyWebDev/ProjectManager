using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMapper _mapper;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository, IMapper mapper)
        {
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
        }
        
        public IEnumerable<CollaboratorDto> GetAll() => 
            _mapper.Map<IEnumerable<Collaborator>, IEnumerable<CollaboratorDto>>(_collaboratorRepository.GetAll());

        public CollaboratorDto Get(Func<Collaborator, bool> predicate) => 
            _mapper.Map<Collaborator, CollaboratorDto>(_collaboratorRepository.Get(predicate));

        public bool Exist(Func<Collaborator, bool> predicate) => _collaboratorRepository.Exist(predicate);
        
        public void Update(CollaboratorDto collaboratorDto) => 
            _collaboratorRepository.Update(_mapper.Map<CollaboratorDto, Collaborator>(collaboratorDto));

        public void Delete(CollaboratorDto collaboratorDto) => 
            _collaboratorRepository.Delete(_mapper.Map<CollaboratorDto, Collaborator>(collaboratorDto));
    }
}