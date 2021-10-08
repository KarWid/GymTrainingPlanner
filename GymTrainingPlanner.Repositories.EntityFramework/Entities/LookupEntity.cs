namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }

        public bool IsActive { get; set; }
        public DateTimeOffset ActiveTo { get; set; }

        public virtual IEnumerable<ExerciseDefinitionEntity> Exercises { get; set; }
    }
}
