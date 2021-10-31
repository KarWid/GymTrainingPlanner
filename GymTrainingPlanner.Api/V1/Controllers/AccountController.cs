namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using GymTrainingPlanner.Common.Managers.V1;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Resources;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;

        public AccountController(
            ILogger<AccountController> logger,
            IUserManager userManager)
            :base (logger)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountInDTO model)
        {
            try
            {
                var result = await _userManager.CreateAccountAsync(model);
                var confirmationUrl = GetEmailConfirmationUrl(result.UserId, result.EmailConfirmationToken);
                await _userManager.SendConfirmationEmailAsync(result.Email, confirmationUrl);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - Register - POST");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail")]
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

        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // TODO @KWidla: to analyze
            try
            {
                await _userManager.SignOut();
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Account - SignOut - GET");
            }

            return Ok();
        }

        private string GetEmailConfirmationUrl(Guid userId, string emailConfirmationToken)
        {
            return Url.Action(
                nameof(AccountController.ConfirmEmail), 
                ControllerContext.ActionDescriptor.ControllerName,
                new { userId = userId, token = emailConfirmationToken }, 
                Request.Scheme);
        }

        //[AllowAnonymous]
        //public IActionResult Authenticate()
        //{
        //    var user = _userManager.Authenticate("test", "test");

        //    if (user == null)
        //    {
        //        return BadRequest(new { message = "Username or password is incorrect" });
        //    }

        //    return Ok(user);
        //}

        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
        //    var result = _userManager.GetAll();

        //    var id = new Guid((string)User.Identity.Name);

        //    return Ok(result);
        //}
    }
}