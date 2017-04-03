using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class CategoryPosts
    {

        public CategoryPosts()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)]
        [Required]
        public string CategoryName { get; set; }

        public virtual ISet<Post> Posts { get; private set; }
    }
}