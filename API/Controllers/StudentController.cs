using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
public class StudentController : GenericBaseController
{

    #region Search
    [HttpGet(Router.StudentRoute.List)]
    public async Task<IActionResult> GetStudentship()
    {
        var response = await _mediator.Send(new GetStudentListQuery());
        return ActionResult(response);
    }

    [HttpPost(Router.StudentRoute.Pagination)]
    public async Task<IActionResult> Pagination([FromBody] GetStudentPaginationListQuery dot)
    {
        var response = await _mediator.Send(dot);
        return ActionResult(response);
    }
    #endregion

    #region Get Single Student by ID
    [HttpGet(Router.StudentRoute.GetById)]
    public async Task<IActionResult> GetSingleStudent([FromRoute] int id)
    {
        //var response = await _mediator.Send(new GetSingleStudentQuery() { Id = id});
        var response = await _mediator.Send(new GetSingleStudentQuery(id));
        return ActionResult(response);
    }
    #endregion

    #region Create
    [HttpPost(Router.StudentRoute.Create)]
    public async Task<IActionResult> Create([FromBody] AddStudentCommand dto)
    {
        var response = await _mediator.Send(dto);
        return ActionResult(response);
    }
    #endregion

    #region Edit
    [HttpPut(Router.StudentRoute.Edit)]
    public async Task<IActionResult> Edit([FromBody] EditStudentCommand dto)
    {
        var response = await _mediator.Send(dto);
        return ActionResult(response);
    }
    #endregion

    #region Delete
    [HttpDelete(Router.StudentRoute.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _mediator.Send(new DeleteStudentCommand(id));
        return ActionResult(response);
    }
    #endregion

}
