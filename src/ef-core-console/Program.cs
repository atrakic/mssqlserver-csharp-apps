using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

using EfCoreConsole.Context;
using EfCoreConsole.Models;

namespace EfCoreConsole
{
    class Program : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        // EF Core uses this method at design time to access the DbContext
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("Default") ?? string.Empty;

            return new ApplicationDbContext(connectionString);
        }

        private static void Main(string[] args)
        {
            Program p = new Program();

            using (ApplicationDbContext context = p.CreateDbContext(args))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.Migrate();

                context.Add(new Movie
                {
                    Name = "Oppenheimer",
                    Description = "A dramatization of the life story of J. Robert Oppenheimer, the physicist who had a large hand in the development of the atomic bomb, thus helping end World War 2. We see his life from university days all the way to post-WW2, where his fame saw him embroiled in political machinations.",
                    ReleaseYear = 2023,
                    RuntimeMinutes = 180
                });
                context.SaveChanges();

                int movieCount = 0;
                foreach (Movie movie in context.Movies)
                {
                    movieCount++;
                    Console.WriteLine($"{movieCount}: {movie.Name} ({movie.ReleaseYear})");
                }
            }
        }
    }
}
