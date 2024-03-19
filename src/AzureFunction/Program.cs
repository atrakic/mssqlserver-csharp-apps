using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Function;

string sqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION") ?? string.Empty;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => {
        s.AddDbContext<BloggingContext>(
            options => options.UseSqlServer(sqlConnectionString)
        );
    })
    .Build();

await host.RunAsync();