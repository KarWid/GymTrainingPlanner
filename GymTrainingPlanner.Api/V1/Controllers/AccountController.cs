namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using GymTrainingPlanner.Common.Managers.V1;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Resources;
    using GymTrainingPlanner.Api.Services;
    using GymTrainingPlanner.Common.Models.Responses;

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(
            ILogger<AccountController> logger, 
            IUserManager userManager,
            IJwtTokenService jwtTokenService)
            :base (logger)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [ProducesResponseType(typeof(ApiResponse<RegisterAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterAccountRequest model)
        {
            try
            {
                var result = await _userManager.CreateUserAccountAsync(model);
                var confirmationUrl = GetEmailConfirmationUrl(result.UserId, result.EmailConfirmationToken);
                await _userManager.SendConfirmationEmailAsync(result.Email, confirmationUrl);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - Register - POST");
            }
        }

        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet, Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                await _userManager.ConfirmEmailAsync(email, token);
                return Ok(GeneralResource.Account_Email_Confirmed);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - ConfirmEmail - Get");
            }
        }

        [HttpGet, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // TODO @KWidla: to analyze
            try
            {
                await _userManager.SignOut();
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - Logout - GET");
            }

            return Ok();
        }

        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost, Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest authenticateRequest)
        {
            try
            {
                var account = await _userManager.AuthenticateAsync(authenticateRequest);
                var authenticationToken = _jwtTokenService.GenerateNewToken(account);
                return Ok(new { AuthorizationToken = authenticationToken });
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - Authenticate - POST");
            }
        }

        private string GetEmailConfirmationUrl(Guid userId, string emailConfirmationToken)
        {
            return Url.Action(
                nameof(AccountController.ConfirmEmail), 
                ControllerContext.ActionDescriptor.ControllerName,
                new { userId = userId, token = emailConfirmationToken }, 
                Request.Scheme);
        }
    }
}