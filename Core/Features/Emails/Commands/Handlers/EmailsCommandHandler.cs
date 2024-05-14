using Core.Features.Emails.Commands.Requests;
using Data.Entities.ThirdParty.MailService.Dtos;

namespace Core.Features.Emails.Commands.Handlers;

public class EmailsCommandHandler : GenericBaseResponseHandler,
    IRequestHandler<SendEmailCommand, GenericBaseResponse<string>>
{
    #region Fields
    private readonly IEmailsService _emailsService;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    #endregion

    #region Constructors
    public EmailsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                IEmailsService emailsService) : base(stringLocalizer)
    {
        _emailsService = emailsService;
        _stringLocalizer = stringLocalizer;
    }
    #endregion

    #region Handle Functions
    public async Task<GenericBaseResponse<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var mail = new EmailDto()
        {
            MailTo = request.MailTo,
            Subject = request.Subject,
            Body = request.Body
        };
        var response = await _emailsService.SendEmail(mail);
        if (response == "Success")
            return Success<string>("");
        return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
    }
    #endregion
}
