using Core.Features.Authentication.Commands.Requests;

namespace API.Controllers;

[ApiController]
public class AuthenticationController : GernericBaseController
{
    #region SignIn
    [HttpPost(Router.Authentication.SignIn)]
    public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    } 
    #endregion

}
