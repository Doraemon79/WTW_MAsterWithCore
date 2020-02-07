using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wtw.Models;
using Wtw.Services;

namespace Wtw
{
    class Program
    {
        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();

            services.AddSingleton(config);
            services.Configure<ApplicationSettings>(config);
            services.AddTransient<TriangleService>();
            services.AddTransient<ConsoleApplication>();

            return services;
        }

        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Wtw <input> <output>");
                return;
            }

            var services = ConfigureServices();

            services.AddSingleton(new CommandLine()
            {
                InputFileName = args[0],
                OutputFileName = args[1]
            });

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<ConsoleApplication>().Run();
        }
    }
}
