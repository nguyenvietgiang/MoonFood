using Microsoft.AspNetCore.Http;

namespace MoonBussiness.CommonBussiness.File
{
    public interface IFileService
    {
        string SaveImage(IFormFile image, string host);
    }
}
