
using Core.Features.Authentication.Commands.Requests;
using Data.Authentication;
using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Services.Interface;

namespace Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : GenericBaseResponseHandler,
        IRequestHandler<SignInCommand, GenericBaseResponse<string>>
    //IRequestHandler<RefreshTokenCommand, GenericBaseResponse<JwtAuthResult>>,
    //IRequestHandler<SendResetPasswordCommand, GenericBaseResponse<string>>,
    //IRequestHandler<ResetPasswordCommand, GenericBaseResponse<string>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;



        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            UserManager<User> userManager,
                                            SignInManager<User> signInManager,
                                            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }


        #endregion

        public async Task<GenericBaseResponse<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);

            //Return The UserName Not Found
            if (user == null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);

            //try To Sign in 
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if Failed Return Passord is wrong
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            //confirm email
            //if (!user.EmailConfirmed)
            //    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);

            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);

            //return Token 
            return Success(result);
        }
    }
}
