using Data.Entities.ThirdParty.UploadDoucments.Dtos;
using Microsoft.AspNetCore.Http;

namespace Data.Entities.ThirdParty.MailService.Dtos;

public class EmailDto
{
    public string MailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public IList<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    public IList<DocumentsInfo> DocumentsList { get; set; } = new List<DocumentsInfo>();
    public IEnumerable<string> MailToList { get; set; } = new List<string>();
    public IEnumerable<string> EmailCC { get; set; } = new List<string>();
    public IEnumerable<string> EmailBCC { get; set; } = new List<string>();
    public string Priority { get; set; }

}
