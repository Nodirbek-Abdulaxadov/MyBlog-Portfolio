using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBlog_Portfolio.Data.Models;
using MyBlog_Portfolio.Data.Services;
using MyBlog_Portfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _environment;

        public PostController(IPostService postService, IWebHostEnvironment environment)
        {
            _postService = postService;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPost(PostCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueName = string.Empty;
                if (createViewModel.ImageFile.FileName != null)
                {
                    string uplodFolder = Path.Combine(_environment.WebRootPath, "Images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + createViewModel.ImageFile.FileName;

                    string filePath = Path.Combine(uplodFolder, uniqueName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    createViewModel.ImageFile.CopyTo(fileStream);
                    fileStream.Close();
                }


                Post post = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = createViewModel.Title,
                    Body = createViewModel.Body,
                    CreatedTime = DateTime.Now,
                    ImageFileName = uniqueName,
                    Category = createViewModel.Category,
                    Region = createViewModel.Region
                };

                post = _postService.AddPost(post);

                return RedirectToAction("PostDetails", post);
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditPost()
        {
            return View();
        }
        
        public IActionResult PostDetails(Post post)
        {
            return View(post);
        }
    }
}
