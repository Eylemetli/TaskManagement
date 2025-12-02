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

        public List<TaskItem> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public TaskItem Add(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public TaskItem? Update(TaskItem task)
        {
            if (!_context.Tasks.Any(t => t.Id == task.Id))
                return null;

            _context.Tasks.Update(task);
            _context.SaveChanges();
            return task;
        }

        public bool Delete(int id)
        {
            var t = _context.Tasks.Find(id);
            if (t == null) return false;

            _context.Tasks.Remove(t);
            _context.SaveChanges();
            return true;
        }
    }
}

