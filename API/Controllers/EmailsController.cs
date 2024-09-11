using Core.Features.Emails.Commands.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class EmailsController : GenericBaseController
    {
        [HttpPost(Router.Emails.SendEmail)]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
        {
            var response = await _mediator.Send(command);
            return ActionResult(response);
        }

    }
}
