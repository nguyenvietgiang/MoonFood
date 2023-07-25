

using Microsoft.AspNetCore.Hosting;
using MoonBussiness.Interface;

namespace MoonBussiness.Repository
{
    public class ExelRepository : IExelRepository
    {
        private readonly string _commonFolderPath;

        public ExelRepository (IWebHostEnvironment env)
        {
            _commonFolderPath = Path.Combine(env.ContentRootPath, "Template");
        }
        public byte[] GetExcelTemplate(string templateName)
        {
            string templateFilePath = Path.Combine(_commonFolderPath, "ExcelTemplate", templateName + "Template.xlsx");

            using (FileStream fileStream = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    fileStream.CopyTo(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
