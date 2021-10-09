namespace GymTrainingPlanner.Repositories.EntityFramework.Configurations
{
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class ExerciseDefinitionConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<ExerciseDefinitionEntity>()
                .Property(_ => _.Id)
                .HasColumnType(Constants.PostgresFunctions.UUID_COLUMN_TYPE)
                .HasDefaultValueSql(Constants.PostgresFunctions.UUID_GENERATE_V4)
                .IsRequired();
        }
    }
}
