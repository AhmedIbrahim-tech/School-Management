using Microsoft.AspNetCore.Http;

namespace Services.Interface;

public interface IFileService
{
    public Task<string> UploadImage(string Location, IFormFile file);
}
