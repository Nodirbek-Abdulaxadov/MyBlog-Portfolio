using Microsoft.EntityFrameworkCore;
using MyBlog_Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.Data.Data_EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
