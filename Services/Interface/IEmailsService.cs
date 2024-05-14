using Data.Entities.ThirdParty.MailService.Dtos;

namespace Services.Interface;

public interface IEmailsService
{
    Task<string> SendEmail(EmailDto dto);//string email, string Message, string? reason);
    Task<bool> SendEmailAsync(EmailDto emailDto);
}
