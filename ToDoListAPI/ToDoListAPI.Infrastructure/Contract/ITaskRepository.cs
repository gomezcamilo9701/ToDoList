using ToDoListAPI.Domain.Models;

namespace ToDoListAPI.Infrastructure.Contract
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAll();
    }
}
