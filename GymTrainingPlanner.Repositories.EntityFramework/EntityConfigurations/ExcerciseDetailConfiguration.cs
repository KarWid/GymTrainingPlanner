namespace GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class ExcerciseDetailConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<ExerciseDetailEntity>().Property(_ => _.AdditionalInformation).HasMaxLength(4000);
        }
    }
}
