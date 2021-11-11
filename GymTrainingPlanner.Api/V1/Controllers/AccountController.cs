namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IUserManager userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [ProducesResponseType(typeof(ApiResponse<RegisterAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterAccountRequest model)
        {
            // TODO @KWidla - change models, ideally it would be when you return RegisterAccountResponse,
            // get RegisterAccountRequest, but service uses model e.g. Account
            var result = await _userManager.CreateUserAccountAsync(model);
            var confirmationUrl = GetEmailConfirmationUrl(result.UserId, result.EmailConfirmationToken);
            await _userManager.SendConfirmationEmailAsync(result.Email, confirmationUrl);

            return Ok(result);
        }

        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet, Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            await _userManager.ConfirmEmailAsync(email, token);
            return Ok(GeneralResource.Account_Email_Confirmed);
        }

        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost, Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest authenticateRequest)
        {
            var account = await _userManager.AuthenticateAsync(authenticateRequest);
            var authenticationToken = _jwtTokenService.GenerateNewToken(account);
            return Ok(new { AuthorizationToken = authenticationToken });
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