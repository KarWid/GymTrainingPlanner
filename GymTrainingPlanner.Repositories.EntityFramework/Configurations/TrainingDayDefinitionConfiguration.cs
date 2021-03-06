namespace GymTrainingPlanner.Repositories.EntityFramework.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    public static class TrainingDayDefinitionConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder
                .Entity<TrainingDayDefinitionEntity>()
                .Property(_ => _.Id)
                .HasColumnType(Constants.PostgresFunctions.UUID_COLUMN_TYPE)
                .HasDefaultValueSql(Constants.PostgresFunctions.UUID_GENERATE_V4)
                .IsRequired();
        }
    }
}
