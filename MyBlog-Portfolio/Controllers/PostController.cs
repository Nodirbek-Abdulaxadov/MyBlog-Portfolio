using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Index(Guid id)
        {
            List<Post> posts = _postService.GetAllPosts().Where(post => post.UserId == id).ToList();
            return View(posts);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }
        [Authorize]
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
                    Region = createViewModel.Region,
                    UserId = createViewModel.UserId
                };

                post = _postService.AddPost(post);

                return RedirectToAction("PostDetails", post);
            }
            else
            {
                return View();
            }
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult EditPost(Guid id)
        {
            Post post = _postService.GetPostById(id);

            PostEditViewModel viewModel = new PostEditViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                ImageFileName = post.ImageFileName,
                Category = post.Category,
                Region = post.Region,
                UserId = post.UserId
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditPost(PostEditViewModel viewModel)
        {
            string img = viewModel.ImageFileName;
            //if (viewModel.newImageFile != "")
            //{
            // Eski rasm o'chirilishi va yangi rasm saqlanishi kerak
            //}

            Post post = new Post()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                Category = viewModel.Category,
                Region = viewModel.Region,
                ImageFileName = img,
                UserId = viewModel.UserId
            };

            post = _postService.UpdatePost(post);

            return RedirectToAction("PostDetails", post);
        }

        public IActionResult PostDetails(Guid id)
        {
            Post post = _postService.GetPostById(id);
            return View("PostDetails", post);
        }

        public IActionResult DeletePost(Guid id)
        {
            _postService.DeletePost(id);

            return RedirectToAction("Index");
        }
    }
}
