namespace GymTrainingPlanner.Repositories.EntityFramework
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Repositories.EntityFramework.EntityConfigurations;

    public class GymTrainingPlannerDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid>
    {
        public DbSet<ExerciseDetailEntity> ExerciseDetails { get; set; }
        public DbSet<ExerciseDefinitionEntity> Exercises { get; set; }
        public DbSet<LookupEntity> Lookups { get; set; }
        public DbSet<TrainingDayDefinitionEntity> TrainingDays { get; set; }
        public DbSet<TrainingPlanEntity> TrainingPlans { get; set; }

        public GymTrainingPlannerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AppUserConfiguration.Configure(builder);
            ExerciseDefinitionConfiguration.Configure(builder);
            LookupConfiguration.Configure(builder);
            TrainingDayConfiguration.Configure(builder);
            TrainingPlanConfiguration.Configure(builder);
        }
    }
}
