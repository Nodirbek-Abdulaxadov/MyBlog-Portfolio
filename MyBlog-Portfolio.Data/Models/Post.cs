using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_Portfolio.Data.Models
{
    [Table("Posts")]
    public class Post
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public string ImageFileName { get; set; }
        [Required]
        public PostCategory Category { get; set; }
        [Required]
        public PostRegion Region { get; set; }
        
        public Guid UserId { get; set; }
    }
}
