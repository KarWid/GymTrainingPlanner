namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class TrainingPlanConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<TrainingPlanEntity>()
                .HasOne<AppUserEntity>()
                .WithMany(_ => _.TrainingPlans)
                .HasForeignKey(_ => _.UserId)
                .IsRequired();

            builder
                .Entity<TrainingPlanEntity>()
                .HasMany<TrainingDayDefinitionEntity>()
                .WithOne(_ => _.TrainingPlan)
                .HasForeignKey(_ => _.TrainingPlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<TrainingPlanEntity>()
                .Property(_ => _.Name)
                .HasMaxLength(100);
        }
    }
}
