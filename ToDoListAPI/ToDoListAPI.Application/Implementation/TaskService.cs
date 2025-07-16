using System.Threading.Tasks;
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

        public async Task<List<TasksDto>> GetAllTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetAllTasks();

                var activeTasks = tasks.Where(x => x.IsDeleted == false).ToList();

                var tasksDto = new List<TasksDto>();
                foreach (var task in activeTasks)
                {
                    var tasksDeserialized = AutoMapperConfig.GetMapper<Tasks, TasksDto>().Map<TasksDto>(task);
                    tasksDto.Add(tasksDeserialized);
                }
                return tasksDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TaskDetailDto?> GetTaskById(long id)
        {
            try
            {
                var taskById = await _taskRepository.GetTaskById(id);
                var taskDetailDto = new TaskDetailDto();

                var taskDes = AutoMapperConfig.GetMapper<Tasks, TaskDetailDto>().Map<TaskDetailDto>(taskById);

                return taskDes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddTask(AddTaskDto taskDto)
        {
            try
            {
                var task = new Tasks
                {
                    State = taskDto.State,
                    Description = taskDto.Description,
                    CreatedBy = "Admin",
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };
                await _taskRepository.AddTask(task);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> EditTask(TasksDto taskDto)
        {
            try
            {
                var existingTask = await _taskRepository.GetTaskById(taskDto.Id);
                if (existingTask == null || existingTask.IsDeleted == true)
                    return false;

                existingTask.State = taskDto.State;
                existingTask.Description = taskDto.Description;
                existingTask.ModifiedBy = "Admin";
                existingTask.ModifiedAt = DateTime.Now;

                await _taskRepository.EditTask(existingTask);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTask(long id)
        {
            try
            {
                var existingTask = await _taskRepository.GetTaskById(id);
                if (existingTask == null || existingTask.IsDeleted == true)
                    return false;

                existingTask.IsDeleted = true;
                existingTask.ModifiedBy = "Admin";
                existingTask.ModifiedAt = DateTime.Now;

                await _taskRepository.EditTask(existingTask);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
