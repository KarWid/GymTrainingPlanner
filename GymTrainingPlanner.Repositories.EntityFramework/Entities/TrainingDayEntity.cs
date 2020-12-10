namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;

    public class TrainingDayEntity : BaseEntity<int>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int TrainingPlanId { get; set; }
        public virtual TrainingPlanEntity TrainingPlan { get; set; }

        public virtual IEnumerable<ExerciseEntity> Exercises { get; set; } 
    }
}
