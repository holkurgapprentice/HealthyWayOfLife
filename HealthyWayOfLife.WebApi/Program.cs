using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyWayOfLife.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
#if (!DEBUG)
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                if (Convert.ToInt32(configuration["IsMigrationEnabled"]) != 0)
                {
                    HealthyWayOfLifeDbContext healthyWayOfLifeDbContext = scope.ServiceProvider.GetService<HealthyWayOfLifeDbContext>();
                    healthyWayOfLifeDbContext.GetService<IMigrator>().Migrate();
                }
#endif

                IConfigurationRepository configurationRepository = scope.ServiceProvider.GetService<IConfigurationRepository>();
                GlobalConfig globalConfig = scope.ServiceProvider.GetService<GlobalConfig>();

                globalConfig.ConfigurationList = await configurationRepository.GetConfigurationList().ConfigureAwait(false);
            }

            await webHost.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .ConfigureServices(serviceCollection => serviceCollection.AddAutofac())
                .UseStartup<Startup>();
    }
}
