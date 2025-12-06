namespace TaskManagement.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }


}
