using Data.Entities.ThirdParty.MailService.Dtos;

namespace Core.Features.Emails.Commands.Requests;

public class SendEmailCommand : EmailDto, IRequest<GenericBaseResponse<string>>
{
}
