namespace GymTrainingPlanner.Common.Models.Dtos.V1.Account
{
    using FluentValidation;
    using GymTrainingPlanner.Common.Constants;
    using GymTrainingPlanner.Common.Resources;

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(_ => _.Email)
                .NotNull().WithMessage(GeneralResource.Required)
                .MaximumLength(256).WithMessage(GeneralResource.Maximum_Length_Error)
                .EmailAddress().WithMessage(GeneralResource.Email_Incorrect_Format);

            RuleFor(_ => _.Password)
                .NotNull()
                .Matches(Constant.Login.PasswordFormat)
                .WithMessage(GeneralResource.Login_Password_InvalidFormat);
        }
    }
}
