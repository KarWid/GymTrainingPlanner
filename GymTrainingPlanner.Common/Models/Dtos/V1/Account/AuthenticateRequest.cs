namespace GymTrainingPlanner.Common.Models.Dtos.V1.Account
{
    using FluentValidation;
    using GymTrainingPlanner.Common.Resources;

    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(_ => _.Email).NotNull().WithMessage(GeneralResource.Required);
            RuleFor(_ => _.Password).NotNull().WithMessage(GeneralResource.Required);
        }
    }
}
