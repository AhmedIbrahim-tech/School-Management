using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        #region Fileds
        private readonly IMediator _mediator;
        #endregion

        #region Constractor
        public StudentController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        #endregion

        #region Handler Function
        [HttpGet("/Student/Search")]
        public async Task<IActionResult> GetListofstudent()
        {
           var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        #endregion

    }
}
