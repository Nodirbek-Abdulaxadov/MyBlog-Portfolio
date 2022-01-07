using Microsoft.AspNetCore.Http;

namespace MyBlog_Portfolio.MVC_Services
{
    public interface IMVCService
    {
        string SaveImage(IFormFile formFile);
        bool DeleteImage(string fileName);
    }
}
