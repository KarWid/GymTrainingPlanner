namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class TrainingDayConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<TrainingDayDefinitionEntity>()
                .HasMany<ExerciseDefinitionEntity>()
                .WithOne(_ => _.TrainingDay)
                .HasForeignKey(_ => _.TrainingDayId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
