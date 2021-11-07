namespace GymTrainingPlanner.Common.Models.Dtos.V1.Account
{
    using FluentValidation;
    using GymTrainingPlanner.Common.Extensions;
    using GymTrainingPlanner.Common.Resources;
    using RepositoryConstant = GymTrainingPlanner.Repositories.EntityFramework.Constants;
    using CommonConstant = GymTrainingPlanner.Common.Constants.Constant;

    public class RegisterAccountRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
    {
        public RegisterAccountRequestValidator()
        {
            RuleFor(_ => _.Email)
                .NotNullMaximumLength(RepositoryConstant.StringLength.DEFAULT_AVERAGE_LENGTH)
                .EmailAddress().WithMessage(GeneralResource.Account_Email_Incorrect_Format);

            RuleFor(_ => _.Password)
                .NotNull().WithMessage(GeneralResource.Required)
                .Matches(CommonConstant.Login.PasswordFormat).WithMessage(GeneralResource.Account_Password_InvalidFormat);

            RuleFor(_ => _.FirstName).MatchesLettersOnlyWithMaximumLength(RepositoryConstant.StringLength.DEFAULT_SHORT_LENGTH);
            RuleFor(_ => _.LastName).MatchesLettersOnlyWithMaximumLength(RepositoryConstant.StringLength.DEFAULT_SHORT_LENGTH);
        }
    }
}
