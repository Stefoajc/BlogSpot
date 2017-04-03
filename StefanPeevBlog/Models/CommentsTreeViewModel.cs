using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class CommentsTreeViewModel
    {
        public HashSet<Comments> CommentsTree { get; set; }

        public Comments CurrentComment { get; set; }
    }
}