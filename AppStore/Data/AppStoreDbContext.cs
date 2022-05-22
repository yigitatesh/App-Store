using AppStore.Models;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Data
{
    public class AppStoreDbContext: DbContext
    {
        public AppStoreDbContext(DbContextOptions<AppStoreDbContext> options) : base(options)
        {

        }

        public DbSet<App> Apps { get; set; }
    }
}
