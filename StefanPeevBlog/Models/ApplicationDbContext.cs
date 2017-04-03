using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StefanPeevBlog.Models
{    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<StefanPeevBlog.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<StefanPeevBlog.Models.CategoryPosts> CategoryPosts { get; set; }

        public System.Data.Entity.DbSet<StefanPeevBlog.Models.Comments> Comments { get; set; }

        //public System.Data.Entity.DbSet<StefanPeevBlog.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StefanPeevBlog.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}