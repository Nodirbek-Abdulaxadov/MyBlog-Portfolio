using Microsoft.AspNetCore.Mvc;
using MyBlog_Portfolio.Data.Models;
using MyBlog_Portfolio.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            List<Post> list = (List<Post>)_postService.GetAllPosts();
            return View("Index", list);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
