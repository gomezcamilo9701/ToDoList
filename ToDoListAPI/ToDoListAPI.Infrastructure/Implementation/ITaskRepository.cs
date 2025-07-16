using ToDoListAPI.Domain.Models;

namespace ToDoListAPI.Infrastructure.Implementation
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAll();
    }
}
