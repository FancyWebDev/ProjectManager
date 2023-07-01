using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public interface ICollaboratorRepository : IRepository<Collaborator> { }

    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly DataContext _dataContext;

        public CollaboratorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public IEnumerable<Collaborator> GetAll()
        {
            return _dataContext.Collaborators
                .Include(collaborator => collaborator.Projects);
        }

        public Collaborator Get(Func<Collaborator, bool> predicate, bool noTracking = false)
        {
            if (noTracking)
            {
                return _dataContext.Collaborators
                    .AsNoTracking()
                    .Include(collaborator => collaborator.Projects)
                    .FirstOrDefault(predicate)
                       ?? throw new IndexOutOfRangeException();
            }
            
            return _dataContext.Collaborators
                .Include(collaborator => collaborator.Projects)
                .FirstOrDefault(predicate) 
                ?? throw new IndexOutOfRangeException();
        }

        public void Create(Collaborator collaborator)
        {
            _dataContext.Add(collaborator);
            Save();
        }

        public void Update(Collaborator collaborator)
        {
            _dataContext.Update(collaborator);
            Save();
        }

        public void Delete(Collaborator collaborator)
        {
            _dataContext.Remove(collaborator);
            Save();
        }

        public bool Exist(Func<Collaborator, bool> predicate) => _dataContext.Collaborators.Any(predicate);

        public void Save() => _dataContext.SaveChanges();
    }
}