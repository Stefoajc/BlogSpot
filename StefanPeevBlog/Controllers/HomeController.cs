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

        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(3);

            ViewBag.MostPopular = db.Posts.Include(p => p.Author).OrderByDescending(p => p.TimesVisited).Take(3);

            return View(posts.ToList());
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}