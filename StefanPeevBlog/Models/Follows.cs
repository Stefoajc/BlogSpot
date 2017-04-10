using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class Follows
    {
        public int FollowID { get; set; }

        public string FollowerID { get; set; }

        public string FollowedID { get; set; }
    }
}