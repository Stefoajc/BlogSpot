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

namespace StefanPeevBlog.Controllers
{
    [Authorize]
    public class CategoryPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoryPosts
        public async Task<ActionResult> Index()
        {
            return View(await db.CategoryPosts.ToListAsync());
        }

        // GET: CategoryPosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPosts categoryPosts = await db.CategoryPosts.FindAsync(id);
            if (categoryPosts == null)
            {
                return HttpNotFound();
            }
            return View(categoryPosts);
        }

        // GET: CategoryPosts/Create
        [Authorize(Roles = "Administrators")]                     //Switch Roles on production only for admins
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]                     //Switch Roles on production only for admins
        public async Task<ActionResult> Create([Bind(Include = "Id,CategoryName")] CategoryPosts categoryPosts)
        {
            if (ModelState.IsValid)
            {
                db.CategoryPosts.Add(categoryPosts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categoryPosts);
        }

        // GET: CategoryPosts/Edit/5
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPosts categoryPosts = await db.CategoryPosts.FindAsync(id);
            if (categoryPosts == null)
            {
                return HttpNotFound();
            }
            return View(categoryPosts);
        }

        // POST: CategoryPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CategoryName")] CategoryPosts categoryPosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryPosts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoryPosts);
        }

        // GET: CategoryPosts/Delete/5
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPosts categoryPosts = await db.CategoryPosts.FindAsync(id);
            if (categoryPosts == null)
            {
                return HttpNotFound();
            }
            return View(categoryPosts);
        }

        // POST: CategoryPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoryPosts categoryPosts = await db.CategoryPosts.FindAsync(id);
            db.CategoryPosts.Remove(categoryPosts);
            await db.SaveChangesAsync();
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
