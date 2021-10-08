namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;

    public class TrainingDayDefinitionEntity : BaseEntity<int>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int TrainingPlanId { get; set; }
        public virtual TrainingPlanEntity TrainingPlan { get; set; }

        public virtual IEnumerable<ExerciseDefinitionEntity> Exercises { get; set; } 
    }
}
