using AulaAPI_Mouts.Models;
using Microsoft.EntityFrameworkCore;

namespace AulaAPI_Mouts.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; } = null!;
    }
}
