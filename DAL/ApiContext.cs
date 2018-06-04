using BlogManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }
    }
}
