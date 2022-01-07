using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MyBlog_Portfolio.MVC_Services
{
    public class MVCService : IMVCService
    {
        private readonly IWebHostEnvironment _environment;
        public MVCService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public bool DeleteImage(string fileName)
        {
            try
            {
                if (fileName is not null)
                {
                    string uplodFolder = Path.Combine(_environment.WebRootPath, "Images");
                    string filePath = Path.Combine(uplodFolder, fileName);
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string SaveImage(IFormFile formFile)
        {
            string uniqueName = string.Empty;
            if (formFile.FileName != null)
            {
                string uplodFolder = Path.Combine(_environment.WebRootPath, "Images");
                uniqueName = Guid.NewGuid().ToString() + "_" + formFile.FileName;

                string filePath = Path.Combine(uplodFolder, uniqueName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                formFile.CopyToAsync(fileStream);
                fileStream.Close();
            }

            return uniqueName;
        }
    }
}
