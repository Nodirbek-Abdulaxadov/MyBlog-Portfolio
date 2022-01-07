using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog_Portfolio.Areas.Identity.Data;
using MyBlog_Portfolio.Data.Models;
using MyBlog_Portfolio.Data.Services;
using MyBlog_Portfolio.MVC_Services;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMVCService _mVCService;

        public PostController(IPostService postService, 
            IMVCService mVCService,
            UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
            _mVCService = mVCService;
        }
        [Authorize]
        public IActionResult Index()
        {
            Guid id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
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
                Post post = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = createViewModel.Title,
                    Body = createViewModel.Body,
                    CreatedTime = DateTime.Now,
                    ImageFileName = _mVCService.SaveImage(createViewModel.ImageFile),
                    Category = createViewModel.Category,
                    Region = createViewModel.Region,
                    UserId = Guid.Parse(_userManager.GetUserId(HttpContext.User))
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
                Region = post.Region
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditPost(PostEditViewModel viewModel)
        {
            string img = viewModel.ImageFileName;
            if (viewModel.newImageFile is not null)
            {
                img = _mVCService.SaveImage(viewModel.newImageFile);
            }

            Post post = new Post()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                Category = viewModel.Category,
                Region = viewModel.Region,
                ImageFileName = img,
                CreatedTime = DateTime.Now,
                UserId = Guid.Parse(_userManager.GetUserId(HttpContext.User))
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

            Post post = _postService.GetPostById(id);
            if (_mVCService.DeleteImage(post.ImageFileName) == true)
            { 
                _postService.DeletePost(id);
            }

            return RedirectToAction("Index");
        }
    }
}
