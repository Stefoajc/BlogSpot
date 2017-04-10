using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comments>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public long TimesVisited { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public int CategoryId { get; set; }

        public string ImagePath { get; set; }

        public bool? IsSpecial { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryPosts Category { get; set; }

        public virtual ISet<Comments> Comments { get; set; }

    }
}