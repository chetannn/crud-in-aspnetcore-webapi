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
    }
}