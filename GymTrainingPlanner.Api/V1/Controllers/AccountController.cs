namespace GymTrainingPlanner.Api.V1.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController, ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUserEntity> _identityUserManager;

        public AccountController(UserManager<AppUserEntity> identityUserManager)
        {
            _identityUserManager = identityUserManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUserEntity { Email = model.Email, EmailConfirmed = true, UserName = model.Email };
            var result = await _identityUserManager.CreateAsync(user, model.Password);

            return Ok(result);
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