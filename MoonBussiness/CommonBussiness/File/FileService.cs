using Microsoft.AspNetCore.Http;

namespace MoonBussiness.CommonBussiness.File
{
    public class FileService : IFileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string SaveImage(IFormFile image, string host)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var uploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "file-uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileExtension = System.IO.Path.GetExtension(image.FileName);
            if (!validExtensions.Contains(fileExtension.ToLower()))
            {
                throw new ArgumentException("File truyền vào phải là ảnh.");
            }

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = System.IO.Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var imageUrl = $"{scheme}://{host}/file-uploads/{fileName}";
            return imageUrl;
        }
    }
}
