
using Core.Features.Authentication.Commands.Requests;
using Data.Entities.Authentication;
using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Services.Interface;

namespace Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : GenericBaseResponseHandler,
        IRequestHandler<SignInCommand, GenericBaseResponse<JwtAuthResult>>
        //IRequestHandler<RefreshTokenCommand, GenericBaseResponse<JwtAuthResult>>
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

        #region Sign in
        public async Task<GenericBaseResponse<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);

            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);

            //try To Sign in 
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if Failed Return Passord is wrong
            if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            //confirm email
            //if (!user.EmailConfirmed)
            //    return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);

            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);

            //return Token 
            return Success(result);
        }
        #endregion

        #region Refresh Token

        //public async Task<GenericBaseResponse<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        //{
            //var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            //var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            //switch (userIdAndExpireDate)
            //{
            //    case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
            //    case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
            //    case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
            //    case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            //}
            //var (userId, expiryDate) = userIdAndExpireDate;
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return NotFound<JwtAuthResult>();
            //}
            //var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            //return Success(result);
        //} 
        
        #endregion
    }
}
