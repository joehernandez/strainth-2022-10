using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strainth.DataService.Data;
using Strainth.DataService.Migrations;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var worker = ActivatorUtilities.CreateInstance<Worker>(host.Services);
        worker.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, configuration) =>
            {
                configuration.Sources.Clear();
                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                configuration.AddUserSecrets<Program>();
            })
            .ConfigureServices((context, services) =>
            {
                var connString = context.Configuration.GetConnectionString("StrainthConnection");
                services.AddDbContext<StrainthContext>(options => 
                    options.UseSqlServer(connString, b => b.MigrationsAssembly("Strainth.DataService.Migrations")));
            });
    }
}