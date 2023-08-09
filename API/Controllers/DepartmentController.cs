namespace API.Controllers
{
    [ApiController]
    public class DepartmentController : GernericBaseController
    {
        #region Get Single Department by ID
        [HttpGet(Router.Department.GetById)]
        public async Task<IActionResult> GetSingleDepartment([FromQuery] GetSingleDepartmentQuery query)
        {
            //var response = await _mediator.Send(new GetSingleDepartmentQuery() { Id = id});
            var response = await _mediator.Send(query);
            return ActionResult(response);
        }
        #endregion
    }
}
