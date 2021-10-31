namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using GymTrainingPlanner.Common.Exceptions;
    using GymTrainingPlanner.Common.Resources;

    public class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleException(Exception ex, string description)
        {
            LogErrorMessage(ex, description);

            if (ex is IdentityApiException)
            {
                var identityApiException = ex as IdentityApiException;
                return BadRequest(identityApiException.GetErrorMessage());
            }

            if (ex is BaseApiException)
            {
                var apiException = ex as BaseApiException;
                return BadRequest(apiException.Message);
            }

            return BadRequest(GeneralResource.Something_Went_Wrong);
        }

        protected void LogErrorMessage(Exception ex, string description)
        {
            if (ex is BaseApiException)
            {
                var baseApiException = ex as BaseApiException;
                _logger.LogError(baseApiException.GetErrorMessage(), description);
            }
            else
            {
                _logger.LogError(ex, description);
            }
        }
    }
}
