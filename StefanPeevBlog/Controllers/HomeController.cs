using System.Web.Mvc;
using System.Data.Entity;
using StefanPeevBlog.Models;
using System.Linq;
using System.Web.Security;

namespace StefanPeevBlog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString)
        {
            var posts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(3);
            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Title.Contains(searchString) || p.Body.Contains(searchString));
            }



            ViewBag.MostPopular = db.Posts.Include(p => p.Author).OrderByDescending(p => p.TimesVisited).Take(3);
            ViewBag.Categories = db.CategoryPosts.Select(cP => new CategoryPostNameAndIdOnly{ CategoryId = cP.CategoryId, CategoryName = cP.CategoryName }).ToList();
            ViewBag.Events = db.Events.Select(e => new EventsHomeViewModel{EventId = e.EventId, EventTitle = e.EventTitle }).ToList();


            return View(posts.ToList());
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}