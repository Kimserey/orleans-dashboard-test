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

            Console.WriteLine(@"
  _____            _     _                         _   _______        _      _____ _ _       
 |  __ \          | |   | |                       | | |__   __|      | |    / ____(_) |      
 | |  | | __ _ ___| |__ | |__   ___   __ _ _ __ __| |    | | ___  ___| |_  | (___  _| | ___  
 | |  | |/ _` / __| '_ \| '_ \ / _ \ / _` | '__/ _` |    | |/ _ \/ __| __|  \___ \| | |/ _ \ 
 | |__| | (_| \__ \ | | | |_) | (_) | (_| | | | (_| |    | |  __/\__ \ |_   ____) | | | (_) |
 |_____/ \__,_|___/_| |_|_.__/ \___/ \__,_|_|  \__,_|    |_|\___||___/\__| |_____/|_|_|\___/ 

");
            Console.WriteLine("Press Enter to close.");
            Console.ReadLine();

            host.StopAsync().Wait();
            host.Dispose();
        }
    }
}
