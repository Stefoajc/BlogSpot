using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StefanPeevBlog.Models;
using Microsoft.AspNet.Identity;

namespace StefanPeevBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Comments
        public async Task<ActionResult> Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.PostCommented);
            return View(await comments.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments/Create
        public ActionResult Create(int postId, int? commentId)
        {
            Comments comment = new Comments();
            comment.PostId = postId;
            comment.CommentId = commentId;
            comment.AuthorId = User.Identity.GetUserId();
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comments comments)
        {
            if (!ModelState.IsValid)
            {
                return View(comments);
            }
            comments.PostedOn = DateTime.Now;
            db.Comments.Add(comments);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Posts", new { id = comments.PostId });
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", comments.AuthorId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comments.PostId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AuthorId,PostId,CommentId,PostedOn,Body")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = comments.PostId });
            }
            return View(comments);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = await db.Comments.FindAsync(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TreeSearch(id);
            Comments comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return new EmptyResult() ;
        }


        /// <summary>
        /// Mark all elements of a tree with given root for deletion (without the root) (RECURSIVE)!!
        /// </summary>
        /// <param name="root"></param>
        /// 
        public void TreeSearch(int root)
        {
            var comments = db.Comments.Where(c => c.CommentId == root).ToList();
            foreach (var item in comments)
            {
                TreeSearch(item.Id);
                db.Comments.Remove(item);
            }
            return;
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
