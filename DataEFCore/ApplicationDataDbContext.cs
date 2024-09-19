using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace DataEFCore
{
    public class ApplicationDataDbContext : DbContext
    {
        public ApplicationDataDbContext(DbContextOptions<ApplicationDataDbContext> options) : base(options) { }

        public DbSet<License> Licenses { get; set; }
    }
}
