namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class LookupConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<LookupEntity>()
                .HasKey(_ => _.Id);

            builder
                .Entity<LookupEntity>()
                .Property(_ => _.Name)
                .HasMaxLength(100);

            builder
                .Entity<LookupEntity>()
                .Property(_ => _.Value)
                .HasMaxLength(255);

            builder
                .Entity<LookupEntity>()
                .Property(_ => _.GroupName)
                .HasMaxLength(100);
        }
    }
}
