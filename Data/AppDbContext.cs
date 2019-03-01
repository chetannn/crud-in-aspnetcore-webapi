using Microsoft.EntityFrameworkCore;
using PostApi.Models;

namespace PostApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
         public DbSet<Post> Posts { get; set; }
         public DbSet<Comment> Comments { get; set; }
         public DbSet<User> Users { get; set; }
         public DbSet<Photo> Photos { get; set; }
         public DbSet<PostFavorite> PostFavorites { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
         {
            builder.Entity<PostFavorite>().HasKey(k => new  { k.PostId, k.UserId });

            builder.Entity<PostFavorite>()
            .HasOne(pf => pf.Post)
            .WithMany(p => p.PostFavorites)
            .HasForeignKey(pt => pt.PostId)
            .OnDelete(DeleteBehavior.Restrict);

             builder.Entity<PostFavorite>()
            .HasOne(pf => pf.User)
            .WithMany(p => p.PostFavorites)
            .HasForeignKey(pt => pt.UserId)
            .OnDelete(DeleteBehavior.Restrict);
         }
    }
}