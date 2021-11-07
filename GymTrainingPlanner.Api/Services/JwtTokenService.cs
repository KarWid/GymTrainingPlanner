namespace GymTrainingPlanner.Api.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using GymTrainingPlanner.Api.Models.Config;
    using GymTrainingPlanner.Common.Helpers;
    using GymTrainingPlanner.Common.Services;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;

    public interface IJwtTokenService
    {
        string GenerateNewToken(UserAccount account);
    }

    class JwtTokenService : IJwtTokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ITimeService _timeService;
        private readonly IConfigurationUtilitiesHelper _configurationUtilitiesHelper;

        public JwtTokenService(
            IOptions<JwtConfig> jwtConfig, 
            IConfigurationUtilitiesHelper configurationUtilitiesHelper, 
            ITimeService timeService)
        {
            _configurationUtilitiesHelper = configurationUtilitiesHelper;
            _jwtConfig = jwtConfig.Value;
            _timeService = timeService;
        }

        public string GenerateNewToken(UserAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configurationUtilitiesHelper.GymTrainingPlannerJwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.JwtIssuer,
                Subject = GetClaimsIdentity(account),
                Expires = _timeService.UtcNow().AddMinutes(_jwtConfig.JwtExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GetClaimsIdentity(UserAccount userAccount)
        {
            var result = userAccount.RoleNames.Select(roleName => new Claim(ClaimTypes.Role, roleName)).ToList();
            result.Add(new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()));

            return new ClaimsIdentity(result.ToArray());
        }
    }
}
