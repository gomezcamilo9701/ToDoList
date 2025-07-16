using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Dto.Task;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<TasksDto>>> GetAll()
        {
            try
            {
                var tasks = await _taskService.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("getById")]
        public async Task<ActionResult<TaskDetailDto>> GetById(long id)
        {
            try
            {
                var task = await _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound("Task not found.");
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(AddTaskDto task)
        {
            try
            {
                await _taskService.AddTask(task);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Edit(TasksDto task)
        {
            try
            {
                var result = await _taskService.EditTask(task);
                if (!result)
                {
                    return NotFound("Task not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var result = await _taskService.DeleteTask(id);
                if (!result)
                {
                    return NotFound("Task not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}