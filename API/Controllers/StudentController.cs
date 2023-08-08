namespace API.Controllers
{
    [ApiController]
    public class StudentController : GernericBaseController
    {
        #region Handler Function

        #region Search
        [HttpGet(Router.Student.List)]
        public async Task<IActionResult> GetStudentship()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }

        [HttpPost(Router.Student.Pagination)]
        public async Task<IActionResult> Pagination([FromBody] GetStudentPaginationListQuery DOT)
        {
            var response = await _mediator.Send(DOT);
            return Ok(response);
        }
        #endregion

        #region Get Single Student by ID
        [HttpGet(Router.Student.GetById)]
        public async Task<IActionResult> GetSingleStudent([FromRoute] int id)
        {
            //var response = await _mediator.Send(new GetSingleStudentQuery() { Id = id});
            var response = await _mediator.Send(new GetSingleStudentQuery(id));
            return ActionResult(response);
        }
        #endregion

        #region Create
        [HttpPost(Router.Student.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand dto)
        {
            var response = await _mediator.Send(dto);
            return ActionResult(response);
        }
        #endregion

        #region Edit
        [HttpPut(Router.Student.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand dto)
        {
            var response = await _mediator.Send(dto);
            return ActionResult(response);
        }
        #endregion

        #region Delete
        [HttpDelete(Router.Student.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand(id));
            return ActionResult(response);
        }
        #endregion

        #endregion
    }
}
