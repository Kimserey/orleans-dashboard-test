using Orleans;
using Orleans.Hosting;
using OrleansDashboardTest.Grains;

namespace OrleansDashboardTest.Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            ISiloHost host = new SiloHostBuilder()
                .UseLocalhostClustering()
                .AddMemoryGrainStorageAsDefault()
                .UseInMemoryReminderService()
                .ConfigureApplicationParts(x =>
                {
                    x.AddApplicationPart(typeof(AccountGrain).Assembly).WithReferences();
                })
                .Build();

            host.StartAsync().Wait();
        }
    }
}
