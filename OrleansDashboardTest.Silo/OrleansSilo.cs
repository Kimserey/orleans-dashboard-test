using Orleans;
using Orleans.Hosting;
using OrleansDashboardTest.Grains;
using Serilog;
using System;
using System.Threading.Tasks;

namespace OrleansDashboardTest.Silo
{
    public class OrleansSilo
    {
        private ISiloHost siloHost;

        public OrleansSilo()
        {
            InstantiateSilo();
        }

        public void InstantiateSilo()
        {
            siloHost = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .UseDashboard()
                    .AddMemoryGrainStorageAsDefault()
                    .UseInMemoryReminderService()
                    .AddSimpleMessageStreamProvider("default", options => options.FireAndForgetDelivery = true)
                    .AddMemoryGrainStorage("PubSubStore")
                    .ConfigureApplicationParts(partManager => partManager.AddApplicationPart(typeof(AccountGrain).Assembly).WithReferences())
                    .ConfigureLogging(logging => logging.AddSerilog())
                    .Build();
        }

        public async Task Run()
        {
            try
            {
                await siloHost.StartAsync();
                Log.Logger.Information($"Successfully started Orleans silo.");
            }
            catch (Exception exc)
            {
                Log.Logger.Error(exc, exc.Message);
            }
        }

        public async Task Stop()
        {
            if (siloHost != null)
            {
                try
                {
                    await siloHost.StopAsync();
                    Log.Logger.Information($"Orleans silo shutdown.");
                }
                catch (Exception exc)
                {
                    Log.Logger.Error(exc, exc.Message);
                }
            }
        }
    }
}
