using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto;
using ToDoListAPI.Domain.Models;
using ToDoListAPI.Infrastructure.Implementation;

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
