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

        public async Task<List<Tasks>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}
