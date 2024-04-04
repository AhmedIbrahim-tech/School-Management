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

    #region Confirm Email

    [HttpGet(Router.Authentication.ConfirmEmail)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
    {
        var response = await _mediator.Send(query);
        return ActionResult(response);
    }

    #endregion

    #region Send Reset Password
    [HttpPost(Router.Authentication.SendResetPasswordCode)]
    public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }
    #endregion

    #region Confirm Reset Password
    [HttpGet(Router.Authentication.ConfirmResetPasswordCode)]
    public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
    {
        var response = await _mediator.Send(query);
        return ActionResult(response);
    } 
    #endregion
    [HttpPost(Router.Authentication.ResetPassword)]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }

}
