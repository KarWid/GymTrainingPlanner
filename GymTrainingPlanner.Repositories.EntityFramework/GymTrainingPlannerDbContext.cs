namespace GymTrainingPlanner.Repositories.EntityFramework
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Repositories.EntityFramework.Configurations;

    public class GymTrainingPlannerDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid>
    {
        public DbSet<ExerciseDefinitionEntity> ExerciseDefinitions { get; set; }
        public DbSet<LookupEntity> Lookups { get; set; }
        public DbSet<TrainingDayDefinitionEntity> TrainingDayDefinitions { get; set; }
        public DbSet<TrainingEntity> Trainings { get; set; }
        public DbSet<TrainingPlanEntity> TrainingPlans { get; set; }

        public GymTrainingPlannerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");

            ExerciseDefinitionConfiguration.Configure(builder);
            TrainingConfiguration.Configure(builder);
            TrainingDayDefinitionConfiguration.Configure(builder);
            TrainingPlanConfiguration.Configure(builder);
        }
    }
}
