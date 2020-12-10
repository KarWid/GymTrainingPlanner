namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System.Collections.Generic;

    public class ExerciseEntity : BaseEntity<long>
    {
        // TODO: check if virtual for lazy loading works, for lists and also for one object
        public int ExerciseNameId { get; set; }
        public virtual LookupEntity ExerciseName { get; set; }

        public int TrainingDayId { get; set; } 
        public virtual TrainingDayEntity TrainingDay { get; set; } 

        public virtual IEnumerable<ExerciseDetailEntity> ExcerciseDetails { get; set; }
    }
}
