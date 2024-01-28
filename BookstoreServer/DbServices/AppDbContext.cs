using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DbServices
{
    public class AppDbContext:DbContext
    {

        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BookModel> Books { get; set; }

        


    }
}
