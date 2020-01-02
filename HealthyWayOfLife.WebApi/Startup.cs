using System.Text;
using Autofac;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using HealthyWayOfLife.Service.Services;
using HealthyWayOfLife.WebApi.Filters;
using HealthyWayOfLife.WebApi.Options;
using HealthyWayOfLife.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
            //Autofac
            containerBuilder.RegisterGeneric(typeof(Scope<>)).As(typeof(IScope<>)).InstancePerDependency();

            containerBuilder.RegisterType<GlobalConfig>().SingleInstance();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TransactionService<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CryptoService>().AsSelf();
            containerBuilder.RegisterType<UserSessionRepository>().As<IUserSessionRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EnumService>().AsSelf();
            containerBuilder.RegisterType<SeedRepository>().As<ISeedRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<BiometryRepository>().As<IBiometryRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LogRepository<HealthyWayOfLifeDbContext>>().As<ILogRepository<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<SeedService>().AsSelf();
            containerBuilder.RegisterType<TransactionService<HealthyWayOfLifeDbContext>>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<WebApiAuthorizationService>().As<AuthorizationService>();
            containerBuilder.RegisterType<TokenService>().As<ITokenService>();
            containerBuilder.RegisterType<AuthenticationHandlerService>().AsSelf();

        }

        public void ConfigureServices(IServiceCollection services)
        {
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
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtSignKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy",
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
            app.UseCors("MyCorsPolicy");
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                SeedService seedService = scope.ServiceProvider.GetService<SeedService>();
                await seedService.StartSeed();
            }
        }
    }
}
