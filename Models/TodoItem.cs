namespace TaskifyApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }                 // 🔹 Primary Key (auto-incremented)
        public string Title { get; set; } = string.Empty; // 🔹 The task description
        public bool IsComplete { get; set; }        // 🔹 Whether the task is done
    }
}
