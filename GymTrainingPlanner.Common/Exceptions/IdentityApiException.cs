namespace GymTrainingPlanner.Common.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Common.Resources;

    public class IdentityApiException : BaseApiException
    {
        public List<IdentityError> IdentityErrors { get; }

        public IdentityApiException(List<IdentityError> identityErrors) 
            : base(GeneralResource.Something_Went_Wrong)
        {
            IdentityErrors = identityErrors;
        }

        public override string GetErrorMessage()
        {
            return string.Join(" ", GetErrorMessages());
        }
        public override List<string> GetErrorMessages()
        {
            if (IdentityErrors == null)
            {
                return new List<string>();
            }

            var errorCodesToNotSend = new List<string> { "DuplicateUserName" };

            return IdentityErrors.Where(_ => !errorCodesToNotSend.Contains(_.Code)).Select(_ => _.Description).ToList();
        }
    }
}
