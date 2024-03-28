namespace API.Controllers;

[ApiController]
public class AuthenticationController : GernericBaseController
{
    #region POST: Sign In

    [HttpPost(Router.Authentication.SignIn)]
    public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }

    #endregion


    #region POST: Refresh Token

    [HttpPost(Router.Authentication.RefreshToken)]
    public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }

    #endregion

    #region GET: Validate Token

    [HttpGet(Router.Authentication.ValidateToken)]
    public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
    {
        var response = await _mediator.Send(query);
        return ActionResult(response);
    }

    #endregion

}
