using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Strainth.DataService.Data;
using Strainth.DataService.Data.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Strainth.DataService.Migrations
{
    public class Worker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly StrainthContext _strainthContext;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, StrainthContext strainthContext)
        {
            _configuration = configuration;
            _logger = logger;
            _strainthContext = strainthContext;
        }

        public void Run()
        {
            ApplyMigrations();

            var environtmentName = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
            if (environtmentName == "Development")
            {
                DevTestData.SeedTestData(_strainthContext);
            }

        }        

        private void ApplyMigrations()
        {
            var connString = _configuration.GetConnectionString("StrainthConnection");
            EnsureDatabase.For.SqlDatabase(connString);
            var upgrader = DeployChanges.To
                .SqlDatabase(connString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                // return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            // return 0;
        }
    }
}
