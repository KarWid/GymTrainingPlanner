namespace GymTrainingPlanner.Common.Extensions
{
    using FluentValidation;
    using GymTrainingPlanner.Common.Constants;
    using GymTrainingPlanner.Common.Resources;

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> MatchesLettersOnlyWithMaximumLength<T>(
            this IRuleBuilderInitial<T, string> rule,
            int maximumLength)
        {
            return rule
                .NotNull().WithMessage(GeneralResource.Required)
                .Matches(Constant.Regex.LETTERS_ONLY).WithMessage(GeneralResource.Invalid_Format_Letters_Only)
                .MaximumLength(maximumLength).WithMessage(GeneralResource.Maximum_Length_Error);
        }

        public static IRuleBuilderOptions<T, string> NotNullMaximumLength<T>(
            this IRuleBuilderInitial<T, string> rule,
            int maximumLength)
        {
            return rule
                .NotNull().WithMessage(GeneralResource.Required)
                .MaximumLength(maximumLength).WithMessage(GeneralResource.Maximum_Length_Error);
        }
    }
}
