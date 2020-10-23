using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BookStore.Api.Entities;

namespace BookStore.Api.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

    }
}