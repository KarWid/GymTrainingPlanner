namespace GymTrainingPlanner.Api.Managers.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GymTrainingPlanner.Api.Services;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public interface IUserManager
    {
        AppUserEntity Authenticate(string username, string password);
        AppUserEntity GetById(Guid id);
        IEnumerable<AppUserEntity> GetAll();
    }

    public class TempUserManager : IUserManager
    {
        private List<AppUserEntity> _users = new List<AppUserEntity>
        {
            new AppUserEntity { Id = Guid.Empty, FirstName = "Test", LastName = "User", UserName = "test", PasswordHash = "test" }
        };

        private readonly ITokenService _tokenService;

        public TempUserManager(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public AppUserEntity Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.UserName == username && x.PasswordHash == password);

            // return null if user not found
            if (user == null)
                return null;

            user.Token = _tokenService.GenerateNewToken(user);

            return user;
        }

        public IEnumerable<AppUserEntity> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.PasswordHash = null;
                return x;
            });
        }

        public AppUserEntity GetById(Guid id)
        {
            return new AppUserEntity();
        }
    }
}
