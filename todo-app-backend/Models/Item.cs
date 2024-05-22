using System.ComponentModel.DataAnnotations;

namespace todo_app_backend.Models
{
    // Represents a single to-do item with title, description, and status
    public class Item
    {
        [Key]
        public string Title { get; set; } // This is now the primary key

        public string Description { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
    }
}
