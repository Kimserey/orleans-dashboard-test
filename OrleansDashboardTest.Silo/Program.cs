using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using OrleansDashboardTest.Grains;
using System;

namespace OrleansDashboardTest.Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            ISiloHost host = new SiloHostBuilder()
                .UseDashboard(opts => {
                    opts.HostSelf = true;
                    opts.Port = 5800;
                })
                .UseLocalhostClustering()
                .AddMemoryGrainStorageAsDefault()
                .UseInMemoryReminderService()
                .ConfigureApplicationParts(x =>
                {
                    x.AddApplicationPart(typeof(AccountGrain).Assembly).WithReferences();
                })
                .ConfigureLogging(x => x.AddConsole())
                .Build();

            host.StartAsync().Wait();

            Console.WriteLine("Press Enter to close.");
            Console.ReadLine();

            host.StopAsync().Wait();
            host.Dispose();
        }
    }
}
