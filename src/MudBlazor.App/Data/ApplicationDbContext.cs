using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using My.Models;

namespace My.Data;


public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    /**
    protected override void OnConfiguring(DbContextOptions optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString,
            options => options.EnableRetryOnFailure());
    }*/

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable(b => b.IsMemoryOptimized());        
    }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
    }
}
