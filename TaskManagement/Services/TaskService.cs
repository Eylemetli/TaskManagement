using TaskManagement.Data;
using TaskManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            if (!await _context.Tasks.AnyAsync(t => t.Id == task.Id))
                return null;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _context.Tasks.FindAsync(id);
            if (t == null) return false;

            _context.Tasks.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


