namespace GymTrainingPlanner.Api.Middleware
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using GymTrainingPlanner.Api.Managers.Impl;
    using GymTrainingPlanner.Api.Models.Config;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public static class JwtAuthorizationMiddleware
    {
        public static void AddJwtAuthorization(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            var serviceProvider = services.BuildServiceProvider();
            var jwtConfig = serviceProvider.GetService<IOptions<JwtConfig>>().Value;

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = OnTokenValidated
                    };

                    cfg.RequireHttpsMetadata = false; // TODO to change
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        //ValidIssuer = jwtConfig.JwtIssuer, // ??
                        //ValidateIssuer = true,
                        //ValidAudience = jwtConfig.JwtIssuer, // ??
                        //ValidateAudience = true,

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = jwtConfig.ValidateLifeTime,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        private static Task OnTokenValidated(TokenValidatedContext context)
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<ITempUserManager>();
            var userId = Guid.Parse(context.Principal.Identity.Name);
            var user = userManager.GetById(userId); // TODO: to change to if exists
            if (user == null)
            {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
            }
            return Task.CompletedTask;
        }
    }
}
