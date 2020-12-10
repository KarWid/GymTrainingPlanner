using System;

namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    public class ExerciseDetailEntity : BaseEntity<Guid>
    {
        public DateTimeOffset Date { get; set; } 
        public int SequenceOrder { get; set; }
        public int RepetitionsEstimated { get; set; }
        public float WeightEstimated { get; set; } // pound
        public int RepetitionsDone { get; set; }
        public float WeightDone { get; set; } // pound 
        public string AdditionalInformation { get; set; }

        public long ExcerciseId { get; set; }
        public virtual ExerciseEntity Exercise { get; set; }
    }
}
