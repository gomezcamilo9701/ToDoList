using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<List<TasksDto>> GetAll() => await _taskService.GetAllTasks();

        [HttpGet]
        [Route("getById")]
        public async Task<TaskDetailDto?> GetById(long id) => await _taskService.GetTaskById(id);

        [HttpPost]
        [Route("add")]
        public async Task Add(AddTaskDto task) => await _taskService.AddTask(task);

        [HttpPut]
        [Route("edit")]
        public async Task<bool> Edit(TasksDto task) => await _taskService.EditTask(task);

        [HttpDelete]
        [Route("delete")]
        public async Task<bool> Delete(long id) => await _taskService.DeleteTask(id);
    }
}
