using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using OrleansDashboardTest.GrainInterfaces;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace OrleansDashboardTest.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            IClusterClient client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(x => x.AddApplicationPart(typeof(IAccount).Assembly).WithReferences())
                .ConfigureLogging(x => x.AddConsole())
                .Build();

            services.AddSingleton<IGrainFactory>(sp => {
                client.Connect().Wait();
                return client;
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

            logger.LogInformation(@"
  _____            _     _                         _   _______        _      _____ _ _            _   
 |  __ \          | |   | |                       | | |__   __|      | |    / ____| (_)          | |  
 | |  | | __ _ ___| |__ | |__   ___   __ _ _ __ __| |    | | ___  ___| |_  | |    | |_  ___ _ __ | |_ 
 | |  | |/ _` / __| '_ \| '_ \ / _ \ / _` | '__/ _` |    | |/ _ \/ __| __| | |    | | |/ _ \ '_ \| __|
 | |__| | (_| \__ \ | | | |_) | (_) | (_| | | | (_| |    | |  __/\__ \ |_  | |____| | |  __/ | | | |_ 
 |_____/ \__,_|___/_| |_|_.__/ \___/ \__,_|_|  \__,_|    |_|\___||___/\__|  \_____|_|_|\___|_| |_|\__|
                                                                                                      
");
        }
    }
}
