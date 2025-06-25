using Microsoft.EntityFrameworkCore;
using TaskifyApi.Models;

namespace TaskifyApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}
