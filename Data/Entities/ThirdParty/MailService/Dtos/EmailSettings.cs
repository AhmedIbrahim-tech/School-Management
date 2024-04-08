namespace Data.Entities.ThirdParty.MailService.Dtos;

public class EmailSettings
{
    public int Port { get; set; }
    public string Host { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}
