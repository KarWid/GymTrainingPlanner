namespace GymTrainingPlanner.Api.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using GymTrainingPlanner.Repositories.EntityFramework;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public static class DbContextHelper
    {
        public static void AddDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContextPool<GymTrainingPlannerDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("GymTrainingPlanner")));
            services.AddIdentity<AppUserEntity, AppRoleEntity>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<GymTrainingPlannerDbContext>();
        }
    }
}
