namespace GymTrainingPlanner.Api.Helpers
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using GymTrainingPlanner.Repositories.EntityFramework;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Common.Constants;

    public static class DbContextHelper
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContextPool<GymTrainingPlannerDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable(Constant.EnvironmentVariablesConstant.CONNECTION_STRING)));
            services.AddIdentity<AppUserEntity, AppRoleEntity>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<GymTrainingPlannerDbContext>();
        }
    }
}
