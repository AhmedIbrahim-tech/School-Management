using Core.Features.Authorization.Commands.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
public class AuthorizationController : GernericBaseController
{
    #region Create Role

    [HttpPost(Router.Authorization.Create)]
    public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }


    #endregion


    #region Edit Role

    [HttpPost(Router.Authorization.Edit)]
    public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }


    #endregion
}
