namespace GymTrainingPlanner.Api.V1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GymTrainingPlanner.Common.Models.Responses;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Extensions;
    using GymTrainingPlanner.Common.Constants;

    public class BaseController : ControllerBase
    {
        private UserAccount _currentUser;

        protected UserAccount CurrentUser
        {
            get
            {
                if (_currentUser == null && HttpContext.Items[Constant.HttpContext.CurrentUser] is UserAccount userAccount)
                {
                    if (userAccount != null && !userAccount.Id.IsEmpty())
                    {
                        _currentUser = userAccount;
                    }
                }

                return _currentUser;
            }
        }

        protected OkObjectResult Ok<T>(T value)
        {
            return base.Ok(new SuccessApiResponse<T>(value));
        }
    }
}
