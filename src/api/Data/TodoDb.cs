using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> optionsBuilder) : base(optionsBuilder) { }
        public DbSet<Todo> Todos => Set<Todo>();
    }
}
