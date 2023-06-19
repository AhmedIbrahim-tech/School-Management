
namespace API.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {

        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Contractor
        public StudentController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        #endregion

        #region Handler Function

        #region Search
        [HttpGet(Router.Student.Search)]
        public async Task<IActionResult> GetListofstudent()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        #endregion

        #region Get Single Student by ID
        [HttpGet(Router.Student.GetById)]
        public async Task<IActionResult> Getsinglestudent([FromRoute] int id)
        {
            //var response = await _mediator.Send(new GetSingleStudentQuery() { Id = id});
            var response = await _mediator.Send(new GetSingleStudentQuery(id));
            return Ok(response);
        }
        #endregion

        #endregion

    }
}
