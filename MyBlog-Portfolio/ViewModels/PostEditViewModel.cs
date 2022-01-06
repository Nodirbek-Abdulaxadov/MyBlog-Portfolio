using Microsoft.AspNetCore.Http;
using MyBlog_Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.ViewModels
{
    public class PostEditViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string ImageFileName { get; set; }
        public IFormFile newImageFile { get; set; }
        [Required]
        public PostCategory Category { get; set; }
        [Required]
        public PostRegion Region { get; set; }
    }
}
