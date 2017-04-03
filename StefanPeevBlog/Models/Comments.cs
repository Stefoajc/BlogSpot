using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class Comments
    {
        public Comments()
        {
            CommentsOnComment = new HashSet<Comments>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public int PostId { get; set; }

        public int? CommentId { get; set; }

        public DateTime PostedOn { get; set; }

        [Required]
        public string Body { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        public virtual ISet<Comments> CommentsOnComment { get; set; }

        [ForeignKey("PostId")]
        public virtual Post PostCommented { get; set; }
    }
}