using Microsoft.EntityFrameworkCore;
using TaskifyApi.Models;

namespace TaskifyApi.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}
