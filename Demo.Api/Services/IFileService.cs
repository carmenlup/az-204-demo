using Demo.Api.Models;

namespace Demo.Api.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
