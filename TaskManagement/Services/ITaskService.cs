using TaskManagement.Models;

namespace TaskManagement.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        TaskItem Add(TaskItem task);
        TaskItem? Update(TaskItem task);
        bool Delete(int id);
    }
}

