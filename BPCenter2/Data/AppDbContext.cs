using BPCenter2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPCenter2.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UsersAccount> UsersAccount { get; set; }

    }
}
