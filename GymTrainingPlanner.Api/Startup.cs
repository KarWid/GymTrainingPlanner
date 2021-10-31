namespace GymTrainingPlanner.Api
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using GymTrainingPlanner.Api.Helpers;
    using GymTrainingPlanner.Api.Middleware;
    using GymTrainingPlanner.Api.Services;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Managers.V1;
    using GymTrainingPlanner.Common.Services;
    using GymTrainingPlanner.Common.Helpers;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Api.Providers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(); // TODO: to allow only for one url
            services.AddControllers()
                .AddFluentValidation(_ => _.RegisterValidatorsFromAssembly(typeof(RegisterAccountInDTOValidator).Assembly));
            
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            ConfigureServicesFromHelpers(services);

            // Helpers
            services.AddSingleton<IConfigurationUtilitiesHelper, ConfigurationUtilitiesHelper>();

            // services
            services.AddTransient<IEmailSenderService, TempEmailSenderService>();

            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<ITokenService, TokenService>();

            //managers
            services.AddScoped<IUserManager, AppUserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, ILoggerFactory loggerFactory*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Training Planner - API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseWhiteListMiddleware(Configuration["AllowedIPs"]);

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            //loggerFactory.AddFile("Logs/GymTrainingPlanner-Api-{Date}.txt");
        }

        private void ConfigureServicesFromHelpers(IServiceCollection services)
        {
            services.ConfigureSwagger();
            services.ConfigureIdentityOptions();
            services.ConfigureApplicationCookies();
            services.AddJwtAuthorization(Configuration);
            services.AddDbContext();
            services.ConfigureMapper();
            services.AddLogging();
            services.ConfigureSmtpClient();

            services.AddTransient<CustomEmailConfirmationTokenProvider<AppUserEntity>>();
        }
    }
}
