using System;
using System.Collections;
using System.IO;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace Superstars.DB
{
    internal class Program
    {
        private static IConfiguration _configuration;

        private static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true)
                        .AddEnvironmentVariables()
                        .Build();

                return _configuration;
            }
        }

        public static int Main(string[] args)
        {
            foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                var name = (string) env.Key;
                var value = (string) env.Value;
                Console.WriteLine("{0}={1}", name, value);
            }

            var connectionString = Configuration["ConnectionStrings:SuperstarsDB"];

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            return 0;
        }
    }
}