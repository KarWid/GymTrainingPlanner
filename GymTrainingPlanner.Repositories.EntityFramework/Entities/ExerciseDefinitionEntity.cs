namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExerciseDefinitionEntity : ExtendedBaseEntity<Guid>
    {
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public float Weight { get; set; }

        [StringLength(4000)]
        public string Notes { get; set; }

        // TODO @KWidla: check if virtual for lazy loading works, for lists and also for one object
        public int ExerciseNameId { get; set; }
        public virtual LookupEntity ExerciseName { get; set; }

        public Guid TrainingDayId { get; set; } 
        public virtual TrainingDayDefinitionEntity TrainingDay { get; set; } 
    }
}
