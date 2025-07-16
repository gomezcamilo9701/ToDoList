using ToDoListAPI.Application.Dto.Task;

namespace ToDoListAPI.Application.Contract
{
    public interface ITaskService
    {
        Task<List<TasksDto>> GetAllTasks();
        Task<TaskDetailDto?> GetTaskById(long id);
        Task AddTask(AddTaskDto taskDto);
        Task<bool> EditTask(TasksDto taskDto);
        Task<bool> DeleteTask(long id);
    }
}
