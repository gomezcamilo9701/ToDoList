using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<List<TasksDto>> GetAll() => await _taskService.GetAll();
    }
}
