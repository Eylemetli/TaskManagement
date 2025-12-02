using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly FakeTaskService _service;

        public TasksController(FakeTaskService service)
        {
            _service = service;
        }

        // GET: api/tasks
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _service.GetById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public IActionResult Add(TaskItem task)
        {
            var created = _service.Add(task);
            return Ok(created);
        }

        // PUT: api/tasks
        [HttpPut]
        public IActionResult Update(TaskItem task)
        {
            var updated = _service.Update(task);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ok = _service.Delete(id);
            if (!ok) return NotFound();
            return Ok(new { message = "Silindi" });
        }
    }
}

