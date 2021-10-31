namespace GymTrainingPlanner.Api.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using GymTrainingPlanner.Common.Constants;
    using GymTrainingPlanner.Repositories.EntityFramework;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Common.MapperConfigurations;
    using GymTrainingPlanner.Api.Providers;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Initializes the database context.
        /// </summary>
        /// <param name="services"></param>
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContextPool<GymTrainingPlannerDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable(Constant.EnvironmentVariablesConstant.CONNECTION_STRING)));
            services.AddIdentity<AppUserEntity, AppRoleEntity>().AddEntityFrameworkStores<GymTrainingPlannerDbContext>();
        }

        /// <summary>
        /// Configures identity options.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                    new TokenProviderDescriptor(typeof(CustomEmailConfirmationTokenProvider<AppUserEntity>)));
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            });
        }

        /// <summary>
        /// Configures application cookies.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApplicationCookies(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/V1/Account/Login";
                options.AccessDeniedPath = "/V1/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        /// <summary>
        /// Configures swagger.
        /// </summary>
        /// <param name="service"></param>
        public static void ConfigureSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0",
                    Title = "Gym Training Planner API",
                    Description = "Gym Training Planner API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = string.Empty,
                        Email = string.Empty,
                        Url = new Uri("https://example.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License - TODO",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. " +
                                  @"Enter 'Bearer' and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                        {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures mapper.
        /// </summary>
        /// <param name="service"></param>
        public static void ConfigureMapper(this IServiceCollection service)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(UserMapperProfile).Assembly);
            });

            service.AddSingleton(configuration.CreateMapper());
        }

        public static void ConfigureSmtpClient(this IServiceCollection service)
        {
            service.AddTransient<SmtpClient>((serviceProvider) =>
            {
                return new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = Environment.GetEnvironmentVariable(
                        Constant.EnvironmentVariablesConstant.EMAIL_TEMP_DIRECTORY)
                };
            });
        }
    }
}
