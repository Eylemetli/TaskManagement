namespace TaskManagement.Models
{
    public class TaskUpdateDto
    {
        public int Id { get; set; } // Güncellenecek görev ID
        public string Title { get; set; } // Başlık
        public string Description { get; set; } // AÇIKLAMA (eksikti, eklendi!)
        public bool IsCompleted { get; set; } // Tamamlandı mı?
    }
}
