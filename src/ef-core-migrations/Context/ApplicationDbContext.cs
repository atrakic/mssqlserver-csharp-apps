using EfCoreConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreConsole.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString = default!;

        public ApplicationDbContext(DbContextOptions options)
             : base(options)
        {
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foo>()
                .ToTable("Foo", t => t.ExcludeFromMigrations());
        }

        //public DbSet<Sample> Sample { get; set; }

        public DbSet<Foo> Foo { get; set; }
    }
}
