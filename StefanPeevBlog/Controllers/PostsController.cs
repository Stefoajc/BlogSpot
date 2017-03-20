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
            return View(db.Posts.Include(p => p.Author)
                .Select(p => new PostIndexViewModel { Id = p.Id, Body = p.Body, Date = p.Date,
                                                        Title = p.Title, UserName = p.Author.UserName, TimesVisited = p.TimesVisited })
                .ToList());
        }

        // GET: Posts/UserPosts/5
        public ActionResult UserPosts(string id)
        {
            ViewBag.TotalViews = db.Posts.Where(p => p.Author.UserName == id).Select(p => p.TimesVisited).Sum();
            return View("Index", db.Posts.Include(p => p.Author).Where(p => p.Author.UserName == id)
                .Select(p => new PostIndexViewModel { Id = p.Id, Body = p.Body, Date = p.Date,
                                                        Title = p.Title, UserName = p.Author.UserName, TimesVisited = p.TimesVisited })
                .ToList());
        }

        [Authorize]
        public ActionResult MyPosts()
        {
            var UserId = User.Identity.GetUserId();
            ViewBag.TotalViews = db.Posts.Where(p => p.AuthorId == UserId).Select(p => p.TimesVisited).Sum();
            return View("Index", db.Posts.Include(p => p.Author).Where(p => p.AuthorId == UserId)
                .Select(p => new PostIndexViewModel { Id = p.Id, Body= p.Body, Date=p.Date,
                                                        Title =p.Title, UserName=p.Author.UserName, TimesVisited = p.TimesVisited } )
                .ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Author).Where(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            post.TimesVisited++;
            db.SaveChanges();
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
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
        public ActionResult Create([Bind(Include = "Id,Title,Body,File")] CreatePostViewModel cpost)
        {
            if (!ModelState.IsValid)
            {
                return View(cpost);
            }

            // Saving the image in the File System
            var fileName = Path.GetFileName(cpost.File.FileName);
            var path = Path.Combine(Server.MapPath("~/ImagesPostUploaded"), fileName);
            cpost.File.SaveAs(path);

            // Saving the Entity in the DataBase
            var dbpath = Path.Combine("~/ImagesPostUploaded", fileName);
            Post post = new Post();
            post.TimesVisited = 0;
            post.Date = DateTime.Now;
            post.AuthorId = User.Identity.GetUserId();
            post.Body = cpost.Body;
            post.Title = cpost.Title;
            post.ImagePath = dbpath;

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        // GET: Posts/Edit/5
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        public ActionResult Edit(int? id)
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

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorizationAttributeWithCustomView(ViewParameter = @"../Views/Shared/Unauthorized.cshtml")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,AuthorId,Date,TimesVisited")] Post post)
        {
            if (post.AuthorId != User.Identity.GetUserId())
            {
                return View("Unauthorized");
            }
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
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
