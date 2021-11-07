namespace GymTrainingPlanner.Common.Models.Dtos.V1.Account
{
    using System;

    public class RegisterAccountResponse
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }

        // TODO @KWidla: uncomment when email will work
        //[JsonIgnore]
        public string EmailConfirmationToken { get; set; }
    }
}
