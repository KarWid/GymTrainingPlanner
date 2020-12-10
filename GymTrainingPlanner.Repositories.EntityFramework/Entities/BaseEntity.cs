namespace GymTrainingPlanner.Repositories.EntityFramework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }

        public Guid ModifiedBy { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
