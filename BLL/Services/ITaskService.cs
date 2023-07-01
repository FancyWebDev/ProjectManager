using System;
using System.Collections.Generic;
using BLL.DTO;
using Task = DAL.Models.Task;

namespace BLL.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll();
        TaskDto Get(Func<Task, bool> predicate);
        bool Exist(Func<Task, bool> predicate);
        void Add(TaskDto taskDto);
        void Update(TaskDto taskDto);
        void Delete(TaskDto taskDto);
    }
}