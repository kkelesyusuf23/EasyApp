using EasyApp.UI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyApp.UI.Context
{
    public class EasyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EasyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BlogFollow entity's relationship configurations
            modelBuilder.Entity<BlogFollow>()
                .HasOne(bf => bf.FollowerUser)
                .WithMany(u => u.Follows)  // AppUser has a collection of BlogFollow for FollowerUser
                .HasForeignKey(bf => bf.FollowerUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Avoid cascading delete for FollowerUser

            modelBuilder.Entity<BlogFollow>()
                .HasOne(bf => bf.FollowedUser)
                .WithMany()  // FollowedUser does not need a navigation property here
                .HasForeignKey(bf => bf.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Set Restrict for FollowedUser to avoid cascade conflicts

            // BlogComment entity's relationship configurations
            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.BlogPost)
                .WithMany(b => b.Comments)
                .HasForeignKey(bc => bc.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);  // Avoid cascading delete for BlogPost

            // BlogLike entity's relationship configurations
            modelBuilder.Entity<BlogLike>()
                .HasOne(bl => bl.BlogPost)
                .WithMany(b => b.Likes)
                .HasForeignKey(bl => bl.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);  // Avoid cascading delete for BlogPost

            // BlogSubscription entity's relationship configurations
            modelBuilder.Entity<BlogSubscription>()
                .HasOne(bs => bs.BlogPost)
                .WithMany(b => b.Subscriptions)
                .HasForeignKey(bs => bs.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);  // Avoid cascading delete for BlogPost


        }

        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogFollow> BlogFollows { get; set; }
        public DbSet<BlogLike> BlogLikes { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogSubscription> BlogSubscriptions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
