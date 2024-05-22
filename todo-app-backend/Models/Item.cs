namespace todo_app_backend.Models
{
    // Represents a single to-do item with title, description, and status
    public class Item
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = String.Empty;

        public string status { get; set; } = string.Empty;
    }
}
