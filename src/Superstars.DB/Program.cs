using System;
using System.IO;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;


namespace Superstars.DB
{
    class Program
    {
        static IConfiguration _configuration;

        public static int Main(string[] args)
        {
            foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                string name = (string)env.Key;
                string value = (string)env.Value;
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

        static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
                }

                return _configuration;
            }
        }
    }
}
