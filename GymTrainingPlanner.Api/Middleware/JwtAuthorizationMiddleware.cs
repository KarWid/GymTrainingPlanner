namespace GymTrainingPlanner.Api.Middleware
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using GymTrainingPlanner.Api.Models.Config;
    using GymTrainingPlanner.Common.Constants;
    using GymTrainingPlanner.Common.Helpers;
    using GymTrainingPlanner.Common.Managers.V1;
    using GymTrainingPlanner.Common.Resources;

    public static class JwtAuthorizationMiddleware
    {
        public static void AddJwtAuthorization(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));

            var serviceProvider = services.BuildServiceProvider();

            var jwtConfig = serviceProvider.GetService<IOptions<JwtConfig>>().Value;
            var configurationUtilitiesHelper = serviceProvider.GetService<IConfigurationUtilitiesHelper>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
                        ValidateIssuer = true, // The issuer is the actual server that created the token
                        ValidateAudience = false, // The receiver of the token is a valid recipient
                        ValidateLifetime = jwtConfig.ValidateLifeTime, // The token has not expired

                        ValidIssuer = jwtConfig.JwtIssuer,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configurationUtilitiesHelper.GymTrainingPlannerJwtKey)),
                        ValidateIssuerSigningKey = true //The signing key is valid and is trusted by the server 
                    };
                });
        }

        private static Task OnTokenValidated(TokenValidatedContext context)
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();
            try
            {
                var userAccount = userManager.GetUserAsync(context.Principal).Result;
                if (userAccount != null)
                {
                    context.HttpContext.Items[Constant.HttpContext.CurrentUser] = userAccount;
                }
                else
                {
                    context.Fail(GeneralResource.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                context.Fail(GeneralResource.Unauthorized);
            }

            return Task.CompletedTask;
        }
    }
}
