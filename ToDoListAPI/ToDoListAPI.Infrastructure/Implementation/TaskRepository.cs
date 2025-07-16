using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Domain.Models;
using ToDoListAPI.Infrastructure.Contract;

namespace ToDoListAPI.Infrastructure.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoListCamiloGomezContext _context;

        public TaskRepository(ToDoListCamiloGomezContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks?> GetTaskById(long id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTask(Tasks task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task EditTask(Tasks task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
