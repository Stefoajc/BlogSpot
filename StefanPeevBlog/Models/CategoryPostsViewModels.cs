using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class CategoryPostsViewModels
    {
    }

    public class CategoryPostNameAndIdOnly
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}