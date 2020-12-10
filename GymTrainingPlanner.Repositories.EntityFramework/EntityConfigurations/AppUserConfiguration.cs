namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class AppUserConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<AppUserEntity>()
                .Property(_ => _.FirstName)
                .HasMaxLength(255);

            builder
                .Entity<AppUserEntity>()
                .Property(_ => _.LastName)
                .HasMaxLength(255);

            builder
                .Entity<AppUserEntity>()
                .Property(_ => _.Token)
                .HasMaxLength(255);
        }
    }
}
