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
    }
}