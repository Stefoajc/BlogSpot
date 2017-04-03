using StefanPeevBlog.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StefanPeevBlog.Models
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public long TimesVisited { get; set; }
    }

    public class CreatePostViewModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string AuthorId { get; set; }

        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [ValidateFileAcceptNull(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public HttpPostedFileBase File { get; set; }
    }

    public class EditPostViewModel 
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public string ImagePath { get; set; }
    }

    public class DetailsPostViewModel
    {
        public Post Post { get; set; }
        public Comments Comment { get; set; }
    }
}