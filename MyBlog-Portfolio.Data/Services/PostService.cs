using Microsoft.AspNetCore.Http;
using MyBlog_Portfolio.Data.Data_EF;
using MyBlog_Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.Data.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _dbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Post AddPost(Post newPost)
        {
            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();

            return newPost;
        }

        public void DeletePost(Guid id)
        {
            Post post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public Post GetPostById(Guid id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public Post UpdatePost(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();

            return post;
        }
    }
}
