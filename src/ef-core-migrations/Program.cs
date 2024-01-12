using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

using EfCoreConsole.Context;
using EfCoreConsole.Models;

using Microsoft.EntityFrameworkCore.SqlServer;

namespace EfCoreConsole
{
    class Program : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // EF Core uses this method at design time to access the DbContext
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION") ?? "";

            if (string.IsNullOrEmpty(connectionString))
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = configurationBuilder.Build();
                connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
            }
            return new ApplicationDbContext(connectionString);
        }

        private static void Main(string[] args)
        {
            Program p = new Program();

            using (ApplicationDbContext context = p.CreateDbContext(args))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("Pending migrations:");
                    foreach (var item in context.Database.GetPendingMigrations())
                    {
                        Console.WriteLine($"Migrating {item} ...");
                        context.Database.Migrate();
                    }
                }
                else
                {
                    Console.WriteLine("No pending migrations.");
                }

                Console.WriteLine("Applied migrations:");
                foreach (var item in context.Database.GetAppliedMigrations())
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
