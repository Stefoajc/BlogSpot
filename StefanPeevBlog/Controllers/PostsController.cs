using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StefanPeevBlog.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using StefanPeevBlog.CustomAuthorization;
using System.IO;

namespace StefanPeevBlog.Controllers
{
    public class PostsController : BaseDbContextController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            ViewBag.DisableCreatelink = false;
            return View(db.Posts.Include(p => p.Author)
                .Select(p => new PostIndexViewModel
                {
                    Id = p.Id,
                    Body = p.Body,
                    Date = p.Date,
                    Title = p.Title,
                    UserName = p.Author.UserName,
                    TimesVisited = p.TimesVisited
                })
                .ToList());
        }

        // GET: Posts/UserPosts/5
        public ActionResult UserPosts(string id) // id used as UserName
        {
            ViewBag.Header = id + "'s Posts";
            ViewBag.DisableCreatelink = true;
            if (db.Posts.Where(p => p.Author.UserName == id).Count() != 0)
            {
                ViewBag.TotalViews = db.Posts.Where(p => p.Author.UserName == id).Select(p => p.TimesVisited).Sum();
            }
            else
            {
                ViewBag.TotalViews = 0;
            }
            return View("Index", db.Posts.Include(p => p.Author).Where(p => p.Author.UserName == id)
                                                                .Select(p => new PostIndexViewModel
                                                                {
                                                                    Id = p.Id,
                                                                    Body = p.Body,
                                                                    Date = p.Date,
                                                                    Title = p.Title,
                                                                    UserName = p.Author.UserName,
                                                                    TimesVisited = p.TimesVisited
                                                                })
                                                                .ToList());
        }

        [Authorize]
        public ActionResult MyPosts()
        {
            ViewBag.DisableCreatelink = false;
            ViewBag.Header = "My Posts";
            var UserId = User.Identity.GetUserId();
            if (db.Posts.Where(p => p.AuthorId == UserId).Count() != 0)
            {
                ViewBag.TotalViews = db.Posts.Where(p => p.AuthorId == UserId).Select(p => p.TimesVisited).Sum();
            }
            else
            {
                ViewBag.TotalViews = 0;
            }

            return View("Index", db.Posts.Include(p => p.Author).Where(p => p.AuthorId == UserId)
                                                                .Select(p => new PostIndexViewModel
                                                                {
                                                                    Id = p.Id,
                                                                    Body = p.Body,
                                                                    Date = p.Date,
                                                                    Title = p.Title,
                                                                    UserName = p.Author.UserName,
                                                                    TimesVisited = p.TimesVisited
                                                                })
                                                                .ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Author).Include(p => p.Comments).Where(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            post.TimesVisited++;
            db.SaveChanges();
            ViewBag.Categories = db.CategoryPosts.ToList();
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            CreatePostViewModel cpostVM = new CreatePostViewModel();
            cpostVM.Categories = new SelectList(db.CategoryPosts.ToList()
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString() , Text = c.CategoryName }),"Value","Text");   
            return View(cpostVM);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult Create([Bind(Include = "Id,Title,Body")] Post post)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(post);
        //    }

        //    post.TimesVisited = 0;
        //    post.Date = DateTime.Now;
        //    post.AuthorId = User.Identity.GetUserId();


        //    db.Posts.Add(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");


        //}

        //POST: Posts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Body,File,SelectedCategoryId")] CreatePostViewModel cpost)
        {
            if (!ModelState.IsValid)
            {
                CreatePostViewModel cpostVM = new CreatePostViewModel();
                cpostVM.Categories = new SelectList(db.CategoryPosts.ToList()
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName }), "Value", "Text");
                return View(cpostVM);
            }
            Post post = new Post();
            if (cpost.File != null)
            {


                // Saving the image in the File System
                var fileName = Path.GetFileName(cpost.File.FileName);

                //----------------------------------- TODO DIRECTORY FOR EVERYPOST
                //var directoryOnFileSystem = "~/ImagesPostUploaded/" + User.Identity.GetUserName();
                //Directory.CreateDirectory(directoryOnFileSystem);
                //-----------------------------------

                var path = Path.Combine(Server.MapPath("~/ImagesPostUploaded/"), fileName);
                cpost.File.SaveAs(path);

                // Saving the Entity in the DataBase
                var dbpath = Path.Combine("~/ImagesPostUploaded", fileName);
                post.ImagePath = dbpath;
            }
            else
            {
                post.ImagePath = null;
            }
            post.TimesVisited = 0;
            post.Date = DateTime.Now;
            post.AuthorId = User.Identity.GetUserId();
            post.Body = cpost.Body;
            post.Title = cpost.Title;
            post.CategoryId = cpost.SelectedCategoryId;

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        // GET: Posts/Edit/5
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        public ActionResult Edit(int? id) // Make ViewModel and Show the Categories ! See CreatePostViewModel
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.AuthorId != User.Identity.GetUserId())
            {
                return View("Unauthorized");
            }
            EditPostViewModel ePost = new EditPostViewModel()
                                          { AuthorId = post.AuthorId, Id = post.Id ,Body = post.Body, CategoryName = post.Category.CategoryName, ImagePath = post.ImagePath, Title = post.Title };
            return View(ePost);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,AuthorId")] EditPostViewModel epost)
        {
            if (epost.AuthorId != User.Identity.GetUserId())
            {
                return View("Unauthorized");
            }
            if (!ModelState.IsValid)
            {
                return View(epost);
            }
            Post post = db.Posts.Find(epost.Id);
            post.Title = epost.Title;
            post.Body = epost.Body;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Posts/Delete/5
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.AuthorId != User.Identity.GetUserId())
            {
                return View("Unauthorized");
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            if (post.AuthorId != User.Identity.GetUserId())
            {
                return View("Unauthorized");
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CategoryAllPosts(int id) // as categoryid
        {
            var posts = db.Posts.Include(p =>p.Author).Where(p => p.CategoryId == id).ToList();
            ViewBag.MostPopular = posts.OrderBy(p => p.TimesVisited);
            ViewBag.Categories = db.CategoryPosts.ToList();

            return View("~/Views/Home/Index.cshtml",posts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
