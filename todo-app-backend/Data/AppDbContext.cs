using Microsoft.EntityFrameworkCore;
using todo_app_backend.Models;

namespace todo_app_backend.Data
{
    // This class Handles database connection and stores "Item" objects
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        // This collection holds items in the database (to-do items)
        public DbSet<Item> Items { get; set; }
    }
}
