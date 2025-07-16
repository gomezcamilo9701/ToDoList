using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto;
using ToDoListAPI.Domain.Models;
using ToDoListAPI.Infrastructure.Contract;

namespace ToDoListAPI.Application.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TasksDto>> GetAll()
        {
            var tasks = await _taskRepository.GetAll();
            var tasksDto = new List<TasksDto>();

            foreach (var task in tasks)
            {
                var tasksDeserialized = AutoMapperConfig.GetMapper<Tasks, TasksDto>().Map<TasksDto>(task);
                tasksDto.Add(tasksDeserialized);
            }
            return tasksDto;
        }
    }
}
