namespace GymTrainingPlanner.Api.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerHelper
    {
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
    }
}
