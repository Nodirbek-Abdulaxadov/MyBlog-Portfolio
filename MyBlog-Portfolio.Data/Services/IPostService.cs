using MyBlog_Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBlog_Portfolio.Data.Services
{
    public interface IPostService
    {
        List<Post> GetAllPosts();
        Post GetPostById(Guid id);
        Post AddPost(Post newPost);
        Post UpdatePost(Post post);
        void DeletePost(Guid id);
    }
}
