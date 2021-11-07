namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using GymTrainingPlanner.Common.Exceptions;
    using GymTrainingPlanner.Common.Resources;
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

        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected OkObjectResult Ok<T>(T value)
        {
            return base.Ok(new SuccessApiResponse<T>(value));
        }

        protected IActionResult HandleException(Exception ex, string description)
        {
            LogErrorMessage(ex, description);

            if (ex is NotFoundApiManagerException notFoundApiManagerException)
            {
                return NotFound(new NotFoundApiResponse(notFoundApiManagerException.Message));
            }

            if (ex is ApiManagerException apiManagerException)
            {
                return BadRequest(new BussinessLogicErrorApiResponse(apiManagerException.Message));
            }

            if (ex is IdentityApiException identityApiException)
            {
                return BadRequest(new BussinessLogicErrorApiResponse(identityApiException.GetErrorMessages()));
            }

            if (ex is BaseApiException baseApiException)
            {
                return BadRequest(new BussinessLogicErrorApiResponse(baseApiException.Message));
            }

            return BadRequest(new BussinessLogicErrorApiResponse(GeneralResource.Something_Went_Wrong));
        }

        protected void LogErrorMessage(Exception ex, string description)
        {
            if (ex is BaseApiException baseApiException)
            {
                _logger.LogError(baseApiException.GetErrorMessage(), description);
                return;
            }

            _logger.LogError(ex, description);
        }
    }
}
