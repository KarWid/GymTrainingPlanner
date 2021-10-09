namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LookupEntity : BaseEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<ExerciseDefinitionEntity> Exercises { get; set; }
        public virtual IEnumerable<TrainingPlanEntity> TrainingPlans { get; set; }
    }
}
