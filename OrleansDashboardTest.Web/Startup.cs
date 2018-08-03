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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}
