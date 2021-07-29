using Microsoft.EntityFrameworkCore;
using Witty.Models;

namespace Witty.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<WittyEntry> WittyEntries { get; set; }
    }
}
