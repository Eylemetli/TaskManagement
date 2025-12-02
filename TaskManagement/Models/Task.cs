namespace TaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }          // Görev ID
        public string Title { get; set; }    // Görev Başlığı
        public string Description { get; set; } // Açıklama
        public bool IsCompleted { get; set; }  // Tamamlandı mı?
    }
}

