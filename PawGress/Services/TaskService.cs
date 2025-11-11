using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;
        private readonly PetService _petService;

        public TaskService(AppDbContext context, PetService petService)
        {
            _context = context;
            _petService = petService;
        }

        // Get all tasks for a user
        public async Task<List<TaskItem>> GetTasksForUserAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        // Add a new task
        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // Complete a task and award XP to a pet
        public async Task<bool> CompleteTaskAsync(int taskId, int petId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null || task.Completed) return false;

            task.Completed = true;

            // Award XP to the pet
            await _petService.AddXPAsync(petId, task.XP);

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a task (optional)
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ New: Get tasks by category
        public async Task<List<TaskItem>> GetTasksByCategoryAsync(string category)
        {
            if (category.ToLower() == "all")
            {
                return await _context.Tasks.ToListAsync();
            }

            return await _context.Tasks
                .Where(t => t.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }
        public async Task<bool> CompleteTaskForUserAsync(int userId, int taskId, int petId)
        {
            var userTask = await _context.UserTasks
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TaskItemId == taskId);

            if (userTask == null) return false;
            if (userTask.Completed) return false;

            userTask.Completed = true;

            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                await _petService.AddXPAsync(petId, task.XP);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<TaskItem>> GetHabitsForUserAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.IsHabit && (t.UserId == 0 || t.UserId == userId))
                .ToListAsync();
        }
    }
}
