namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class ExerciseDefinitionConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<ExerciseDefinitionEntity>()
                .HasMany<ExerciseDetailEntity>()
                .WithOne(_ => _.Exercise)
                .HasForeignKey(_ => _.ExcerciseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<ExerciseDefinitionEntity>()
                .HasOne<LookupEntity>()
                .WithMany(_ => _.Exercises)
                .HasForeignKey(_ => _.ExerciseNameId);
        }
    }
}
