namespace GymTrainingPlanner.Common.Exceptions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Common.Resources;

    public class IdentityApiException : BaseApiException
    {
        public IEnumerable<IdentityError> IdentityErrors { get; }

        public IdentityApiException(IEnumerable<IdentityError> identityErrors) 
            : base(GeneralResource.Something_Went_Wrong)
        {
            IdentityErrors = identityErrors;
        }

        public override string GetErrorMessage()
        {
            var result = string.Empty;
            foreach(var identityError in IdentityErrors)
            {
                // username is the same as email, so we do not have to provide this information,
                // because there will be both IdentityErrors - DuplicateUserName and DuplicateEmail
                if (identityError.Code == "DuplicateUserName")
                {
                    continue;
                }

                result += $"{identityError.Description} ";
            }

            return result;
        }
    }
}
