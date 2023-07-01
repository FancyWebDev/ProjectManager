using System;
using System.Collections.Generic;
using BLL.DTO;
using DAL.Models;

namespace BLL.Services
{
    public interface ICollaboratorService
    {
        IEnumerable<CollaboratorDto> GetAll();
        CollaboratorDto Get(Func<Collaborator, bool> predicate);
        bool Exist(Func<Collaborator, bool> predicate);
        void Update(CollaboratorDto collaboratorDto);
        void Delete(CollaboratorDto collaboratorDto);
    }
}