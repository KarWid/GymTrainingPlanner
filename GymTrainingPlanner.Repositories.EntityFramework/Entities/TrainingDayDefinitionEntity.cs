namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;

    public class TrainingDayDefinitionEntity : ExtendedBaseEntity<Guid>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int? JimWendler531MainLiftsId { get; set; }
        public virtual LookupEntity JimWendler531MainLifts { get; set; }

        public Guid TrainingPlanId { get; set; }
        public virtual TrainingPlanEntity TrainingPlan { get; set; }

        public virtual IEnumerable<ExerciseDefinitionEntity> Exercises { get; set; } 
        public virtual IEnumerable<TrainingEntity> Trainings { get; set; }
    }
}
