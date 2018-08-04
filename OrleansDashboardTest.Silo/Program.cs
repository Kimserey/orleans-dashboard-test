using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace OrleansDashboardTest.Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(configuration.GetSection("serilog"))
                .CreateLogger();

            var host = new OrleansSilo();
            host.Run().Wait();

            Console.WriteLine(Title.Value);
            Console.WriteLine("Press Enter to close.");
            Console.ReadLine();

            host.Stop().Wait();
        }
    }
}
