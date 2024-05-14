using Core.Features.Authorization.Queries.Requests;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
public class AuthorizationController : GernericBaseController
{

    [HttpGet(Router.Authorization.RoleList)]
    public async Task<IActionResult> GetRoleList()
    {
        var response = await _mediator.Send(new GetRolesListQuery());
        return ActionResult(response);
    }
    [SwaggerOperation(Summary = "idالصلاحية عن طريق ال", OperationId = "RoleById")]
    [HttpGet(Router.Authorization.GetRoleById)]
    public async Task<IActionResult> GetRoleById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetRoleByIdQuery() { Id = id });
        return ActionResult(response);
    }



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

    #region Delete Role ByID
    [HttpDelete(Router.Authorization.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _mediator.Send(new DeleteRoleCommand(id));
        return ActionResult(response);
    }
    #endregion

    #region Manage User Roles
    [SwaggerOperation(Summary = " ادارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
    [HttpGet(Router.Authorization.ManageUserRoles)]
    public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
    {
        var response = await _mediator.Send(new ManageUserRolesQuery() { UserId = userId });
        return ActionResult(response);
    }
    #endregion
    
    #region Update User Roles
    [SwaggerOperation(Summary = " تعديل صلاحيات المستخدمين", OperationId = "UpdateUserRoles")]
    [HttpPut(Router.Authorization.UpdateUserRoles)]
    public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    }
    #endregion

    #region Manage User Claims
    [SwaggerOperation(Summary = " ادارة صلاحيات الاستخدام المستخدمين", OperationId = "ManageUserClaims")]
    [HttpGet(Router.Authorization.ManageUserClaims)]
    public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
    {
        var response = await _mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
        return ActionResult(response);
    }
    #endregion
    
    #region Update User Claims
    [SwaggerOperation(Summary = " تعديل صلاحيات  الاستخدام المستخدمين", OperationId = "UpdateUserClaims")]
    [HttpPut(Router.Authorization.UpdateUserClaims)]
    public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
    {
        var response = await _mediator.Send(command);
        return ActionResult(response);
    } 
    #endregion




}
