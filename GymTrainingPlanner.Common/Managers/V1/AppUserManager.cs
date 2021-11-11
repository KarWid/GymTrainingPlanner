namespace GymTrainingPlanner.Common.Managers.V1
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Common.Exceptions;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Services;
    using GymTrainingPlanner.Common.Resources;
    using GymTrainingPlanner.Repositories.EntityFramework.Enums;

    public interface IUserManager
    {
        /// <summary>
        /// Creates an account based on model and returns email confirmation token.
        /// </summary>
        /// <param name="registerAccountRequest"></param>
        /// <returns></returns>
        Task<RegisterAccountResponse> CreateUserAccountAsync(RegisterAccountRequest registerAccountRequest);
        
        /// <summary>
        /// Sends confirmation email with confirmation url to a user's email defined by userEmail.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="confirmationUrl"></param>
        /// <returns></returns>
        Task SendConfirmationEmailAsync(string userEmail, string confirmationUrl);

        /// <summary>
        /// Confirms user's email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="confirmationToken"></param>
        /// <returns>Confirmation email succeeded.</returns>
        Task<bool> ConfirmEmailAsync(string email, string confirmationToken);

        /// <summary>
        /// Determines if authentication succeeded.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account information.</returns>
        Task<UserAccount> AuthenticateAsync(AuthenticateRequest model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns>User account with user's roles.</returns>
        Task<UserAccount> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Account Id.</param>
        /// <returns>User account with user's roles.</returns>
        Task<UserAccount> GetUserAsync(Guid userId);
    }

    public class AppUserManager : IUserManager
    {
        private readonly UserManager<AppUserEntity> _identityUserManager;
        private readonly SignInManager<AppUserEntity> _signInManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;

        public AppUserManager(
            UserManager<AppUserEntity> identityUserManager,
            SignInManager<AppUserEntity> signInManager,
            IMapper mapper, 
            IEmailSenderService emailSenderService)
        {
            _identityUserManager = identityUserManager;
            _emailSenderService = emailSenderService;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        #region Public Methods
        /// <inheritdoc cref="IUserManager"/>
        public async Task<RegisterAccountResponse> CreateUserAccountAsync(RegisterAccountRequest model)
        {
            var appUserEntity = _mapper.Map<AppUserEntity>(model);

            var createAccountResult = await _identityUserManager.CreateAsync(appUserEntity, model.Password);
            if (!createAccountResult.Succeeded)
            {
                throw new IdentityApiException(createAccountResult.Errors.ToList());
            }

            var addToRoleResult = await _identityUserManager.AddToRoleAsync(appUserEntity, AppRoleType.User.ToString());
            if (!addToRoleResult.Succeeded)
            {
                throw new IdentityApiException(createAccountResult.Errors.ToList());
            }

            var result = _mapper.Map<RegisterAccountResponse>(appUserEntity);
            result.EmailConfirmationToken = await _identityUserManager.GenerateEmailConfirmationTokenAsync(appUserEntity);

            return result;
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            await _emailSenderService.SendAsync(
                email,
                GeneralResource.Email_ConfirmationEmail_Subject,
                GetBodyForConfirmationEmail(confirmationUrl));
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task<bool> ConfirmEmailAsync(string email, string confirmationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApiManagerException(GeneralResource.Account_Email_Undefined);
            }

            if (string.IsNullOrEmpty(confirmationToken))
            {
                throw new ApiManagerException(GeneralResource.Account_ConfirmationToken_Invalid);
            }

            var user = await _identityUserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ApiManagerException(GeneralResource.Account_NotFound);
            }

            var result = await _identityUserManager.ConfirmEmailAsync(user, confirmationToken);
            if (!result.Succeeded)
            {
                throw new IdentityApiException(result.Errors.ToList());
            }

            return true;
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task<UserAccount> AuthenticateAsync(AuthenticateRequest model)
        {
            await ValidateAuthenticateRequestAsync(model);

            var appUserEntity = await _identityUserManager.FindByNameAsync(model.Email);

            return await GetUserAccountAsync(appUserEntity);
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task<UserAccount> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await GetUserAsync(new Guid(userId));
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task<UserAccount> GetUserAsync(Guid userId)
        {
            var userAccountEntity = await _identityUserManager.FindByIdAsync(userId.ToString());
            if (userAccountEntity == null)
            {
                throw new NotFoundApiManagerException(GeneralResource.Account_NotFound);
            }

            return await GetUserAccountAsync(userAccountEntity);
        }

        #endregion

        #region Private Methods
        private string GetBodyForConfirmationEmail(string confirmationUrl)
        {
            return $"Press this url {confirmationUrl} to confirm your email address.";
        }

        private async Task ValidateAuthenticateRequestAsync(AuthenticateRequest authenticateRequest)
        {
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(
                authenticateRequest.Email, authenticateRequest.Password, true, false);

            if (!passwordSignInResult.Succeeded)
            {
                if (passwordSignInResult.IsLockedOut)
                {
                    throw new ApiManagerException(GeneralResource.Account_Locked);
                }

                if (passwordSignInResult.IsNotAllowed)
                {
                    throw new ApiManagerException(GeneralResource.Account_Authenticate_NotAllowed);
                }

                throw new ApiManagerException(GeneralResource.Account_Authenticate_WrongCredentials);
            }
        }

        private async Task<UserAccount> GetUserAccountAsync(AppUserEntity appUserEntity)
        {
            var roleNames = await _identityUserManager.GetRolesAsync(appUserEntity);

            var result = _mapper.Map<UserAccount>(appUserEntity);
            result.RoleNames = roleNames.ToList();

            return result;
        }
        #endregion
    }
}
