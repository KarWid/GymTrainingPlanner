namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class ExerciseConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<ExerciseEntity>()
                .HasMany<ExerciseDetailEntity>()
                .WithOne(_ => _.Exercise)
                .HasForeignKey(_ => _.ExcerciseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<ExerciseEntity>()
                .HasOne<LookupEntity>()
                .WithMany(_ => _.Exercises)
                .HasForeignKey(_ => _.ExerciseNameId);
        }
    }
}
