namespace Core.Features.Authentication.Queries.Handlers;

public class AuthenticationQueryHandler : GenericBaseResponseHandler,
    IRequestHandler<AuthorizeUserQuery, GenericBaseResponse<string>>,
    IRequestHandler<ConfirmEmailQuery, GenericBaseResponse<string>>,
    IRequestHandler<ConfirmResetPasswordQuery, GenericBaseResponse<string>>
{


    #region Fields
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IAuthenticationServiceAsync _authenticationService;

    #endregion

    #region Constructors
    public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                        IAuthenticationServiceAsync authenticationService) : base(stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
        _authenticationService = authenticationService;
    }


    #endregion

    #region Handle Functions

    #region Validate Token
    public async Task<GenericBaseResponse<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ValidateToken(request.AccessToken);
        if (result == "NotExpired")
            return Success(result);
        return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
    }

    #endregion

    #region Confirm Email
    public async Task<GenericBaseResponse<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
    {
        var confirmEmail = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
        if (confirmEmail == "ErrorWhenConfirmEmail")
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorWhenConfirmEmail]);
        return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailDone]);
    }
    #endregion

    #region Confirm Reset Password
    public async Task<GenericBaseResponse<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
        switch (result)
        {
            case "UserNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
            case "Success": return Success<string>("");
            default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
        }
    }
    #endregion

    #endregion
}
