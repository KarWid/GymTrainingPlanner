namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System.Collections.Generic;

    public class ExerciseDefinitionEntity : BaseEntity<long>
    {
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public float Weight { get; set; }
        public string Notes { get; set; }

        // TODO @KWidla: check if virtual for lazy loading works, for lists and also for one object
        public int ExerciseNameId { get; set; }
        public virtual LookupEntity ExerciseName { get; set; }

        public int TrainingDayId { get; set; } 
        public virtual TrainingDayDefinitionEntity TrainingDay { get; set; } 

        // TODO @KWidla
        //public virtual IEnumerable<ExerciseDetailEntity> ExcerciseDetails { get; set; }
    }
}
