namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using GymTrainingPlanner.Api.Managers.Impl;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        public IActionResult Authenticate()
        {
            var user = _userManager.Authenticate("test", "test");

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userManager.GetAll();

            var id = new Guid((string)User.Identity.Name);

            return Ok(result);
        }
    }
}