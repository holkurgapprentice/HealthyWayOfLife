using Autofac;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Interfaces.Security;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using HealthyWayOfLife.Service.Services;
using HealthyWayOfLife.Service.Services.Security;
using HealthyWayOfLife.WebApi.Filters;
using HealthyWayOfLife.WebApi.Options;
using HealthyWayOfLife.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace HealthyWayOfLife.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(Scope<>)).As(typeof(IScope<>)).InstancePerDependency();

            containerBuilder.RegisterType<GlobalConfig>().SingleInstance();

            containerBuilder.RegisterType<BiometryRepository>().As<IBiometryRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LogRepository<HealthyWayOfLifeDbContext>>().As<ILogRepository<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<SeedRepository>().As<ISeedRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UserSessionRepository>().As<IUserSessionRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            containerBuilder.RegisterType<CryptoService>().AsSelf();
            containerBuilder.RegisterType<EnumService>().AsSelf();
            containerBuilder.RegisterType<SeedService>().AsSelf();
            containerBuilder.RegisterType<TransactionService<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TransactionService<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TokenService>().As<ITokenService>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtTokenConfigurationService = new JwtTokenConfigurationService(Configuration);
            services.AddSingleton<JwtTokenConfigurationService>(jwtTokenConfigurationService);

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(HwolValidationFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<HealthyWayOfLifeDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b =>
                    b.MigrationsAssembly("HealthyWayOfLife.Repository")));
            services.AddSingleton<SeedRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfigureOptions<ExceptionHandlerOptions>, WebApiExceptionHandlerOptions>();
            services.AddScoped<IExceptionService, WebApiExceptionService>();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = jwtTokenConfigurationService.GetValidationParameters();
                });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsHwol",
                    builder => builder
#if DEBUG
                        .WithOrigins("https://localhost:4200", "http://localhost:4200")
#endif
#if RELEASE
                      .SetIsOriginAllowedToAllowWildcardSubdomains()
                      .AllowAnyOrigin()
#endif

                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .Build()
                );
            });
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Add-Migration Name
            // Update-Database
            app.UseExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseExceptionHandler();
            app.UseCors("CorsHwol");
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                SeedService seedService = scope.ServiceProvider.GetService<SeedService>();
                await seedService.StartSeed();
            }
        }
    }
}
