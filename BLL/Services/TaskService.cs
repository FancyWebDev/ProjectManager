using AutoMapper;
using BLL.DTO;
using DAL.Repository;
using Task = DAL.Models.Task;

namespace BLL.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public TaskService(ITaskRepository taskRepository, IMapper mapper, ICollaboratorRepository collaboratorRepository)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _collaboratorRepository = collaboratorRepository;
    }
        
    public IEnumerable<TaskDto> GetAll() => 
        _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(_taskRepository.GetAll());

    public TaskDto Get(Func<Task, bool> predicate) => 
        _mapper.Map<Task, TaskDto>(_taskRepository.Get(predicate));

    public bool Exist(Func<Task, bool> predicate) => _taskRepository.Exist(predicate);

    public void Add(TaskDto taskDto)
    {
        var task = new Task
        {
            Name = taskDto.Name,
            Author = _collaboratorRepository.Get(collaborator => collaborator.Id == taskDto.Author.Id),
            AuthorId = taskDto.Author.Id,
            PerformerId = taskDto.Performer?.Id,
            Comment = taskDto.Comment,
            Status = taskDto.Status,
            Priority = taskDto.Priority
        };

        if (taskDto.Performer != null)
            task.Performer = _collaboratorRepository.Get(collaborator => collaborator.Id == taskDto.Performer.Id);
            
        _taskRepository.Create(task);   
    }

    public void Update(TaskDto taskDto) =>
        _taskRepository.Update(_mapper.Map<TaskDto, Task>(taskDto));

    public void Delete(TaskDto taskDto) => 
        _taskRepository.Delete(_mapper.Map<TaskDto, Task>(taskDto));
}