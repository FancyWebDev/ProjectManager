using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Task = DAL.Models.Task;

namespace DAL.Repository
{
    public interface ITaskRepository : IRepository<Task> { }

    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _dataContext;

        public TaskRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Task> GetAll()
        {
            return _dataContext.Tasks
                .Include(task => task.Author)
                .Include(task => task.Performer);
        }

        public Task Get(Func<Task, bool> predicate, bool noTracking = false)
        {
            return _dataContext.Tasks
                .Include(task => task.Author)
                .Include(task => task.Performer)
                .FirstOrDefault(predicate)
                ?? throw new IndexOutOfRangeException();
        }

        public void Create(Task task)
        {
            _dataContext.Add(task);
            Save();
        }

        public void Update(Task task)
        {
            _dataContext.Update(task);
            Save();
        }

        public void Delete(Task task)
        {
            _dataContext.Remove(task);
            Save();
        }

        public bool Exist(Func<Task, bool> predicate) => _dataContext.Tasks.Any(predicate);

        public void Save() => _dataContext.SaveChanges();
    }
}