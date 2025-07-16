using ToDoListAPI.Application.Dto;

namespace ToDoListAPI.Application.Contract
{
    public interface ITaskService
    {
        Task<List<TasksDto>> GetAll();
    }
}
