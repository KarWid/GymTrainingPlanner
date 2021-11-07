namespace GymTrainingPlanner.Common.Models.Dtos.V1.Account
{
    using System;
    using System.Collections.Generic;

    public class UserAccount
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
