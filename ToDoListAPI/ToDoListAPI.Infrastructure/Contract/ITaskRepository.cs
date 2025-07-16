using ToDoListAPI.Domain.Models;

namespace ToDoListAPI.Infrastructure.Contract
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks?> GetTaskById(long id);
        Task AddTask(Tasks task);
        Task EditTask(Tasks task);
    }
}
