using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StefanPeevBlog.Migrations;

namespace StefanPeevBlog.Models
{    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Post> Posts { get; set; }

        public System.Data.Entity.DbSet<CategoryPosts> CategoryPosts { get; set; }

        public System.Data.Entity.DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithMany()
                .Map(m =>
                    {
                        m.ToTable("Follows");
                        m.MapLeftKey("Followed");
                        m.MapRightKey("Follower");
                    });


            modelBuilder.Entity<CategoryPosts>()
                .HasMany(c => c.Posts)
                .WithRequired(p => p.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<StefanPeevBlog.Models.Events> Events { get; set; }
       
    }
}