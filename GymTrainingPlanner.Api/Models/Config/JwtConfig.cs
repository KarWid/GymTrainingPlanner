namespace GymTrainingPlanner.Api.Models.Config
{
    public class JwtConfig
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireMinutes {get; set;}
        public bool ValidateLifeTime { get; set; }
    }
}
