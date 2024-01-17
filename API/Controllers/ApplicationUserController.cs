using Core.Features.ApplicationUser.Commands.Requests;
using Core.Features.ApplicationUser.Queries.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : GernericBaseController
    {

        #region Paginated User
        [HttpGet(Router.User.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        #endregion

        #region Get User By ID
        [HttpGet(Router.User.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return ActionResult(result);
        }

        #endregion
        
        #region Create User
        [HttpPost(Router.User.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return ActionResult(response);
        }

        #endregion

        #region Edit User
        [HttpPut(Router.User.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand command)
        {
            var response = await _mediator.Send(command);
            return ActionResult(response);
        }
        #endregion

        #region Delete User
        [HttpDelete(Router.User.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return ActionResult(await _mediator.Send(new DeleteUserCommand(id)));
        } 
        #endregion

        #region Change Password
        [HttpPut(Router.User.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return ActionResult(response);
        } 
        #endregion
    }
}
