namespace GymTrainingPlanner.Common.Managers.V1
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;
    using GymTrainingPlanner.Common.Exceptions;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Common.Services;
    using GymTrainingPlanner.Common.Resources;

    public interface IUserManager
    {
        /// <summary>
        /// Creates an account based on model and returns email confirmation token.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="emailConfirmationToken"></param>
        /// <returns></returns>
        Task<RegisterAccountOutDTO> CreateAccountAsync(RegisterAccountInDTO model);
        
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
        /// 
        /// </summary>
        /// <returns></returns>
        Task SignOut();
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
        public async Task<RegisterAccountOutDTO> CreateAccountAsync(RegisterAccountInDTO model)
        {
            var appUserEntity = _mapper.Map<AppUserEntity>(model);

            var createAccountResult = await _identityUserManager.CreateAsync(appUserEntity, model.Password);
            if (!createAccountResult.Succeeded)
            {
                throw new IdentityApiException(createAccountResult.Errors);
            }

            var result = _mapper.Map<RegisterAccountOutDTO>(appUserEntity);
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
                throw new IdentityApiException(result.Errors);
            }

            return true;
        }

        /// <inheritdoc cref="IUserManager"/>
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region Private Methods
        private string GetBodyForConfirmationEmail(string confirmationUrl)
        {
            return $"Press this url {confirmationUrl} to confirm your email address.";
        }
        #endregion
    }
}
