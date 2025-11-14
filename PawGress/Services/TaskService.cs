using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetUserTasksAsync(string username)
        {
            return await _context.Tasks
                .Where(t => t.UserName == username)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var t = await _context.Tasks.FindAsync(id);
            if (t != null)
            {
                _context.Tasks.Remove(t);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteTaskAsync(TaskItem task)
        {
            task.IsCompleted = true;
            await _context.SaveChangesAsync();
        }
    }
}