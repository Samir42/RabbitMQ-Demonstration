using Microsoft.EntityFrameworkCore;
using UserService.API.Entities;

namespace UserService.API.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}