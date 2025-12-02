using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class FakeTaskService
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem Add(TaskItem task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return task;
        }

        public TaskItem GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem Update(TaskItem task)
        {
            var existing = GetById(task.Id);
            if (existing == null) return null;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;
            return existing;
        }

        public bool Delete(int id)
        {
            var t = GetById(id);
            if (t == null) return false;
            tasks.Remove(t);
            return true;
        }
    }
}
