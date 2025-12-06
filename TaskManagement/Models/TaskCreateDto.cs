using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }

}
