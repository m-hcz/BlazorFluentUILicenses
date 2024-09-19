using DataEFCore;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Test.Licenses
{
    public class TestAppDataDbContextFactory : IDbContextFactory<ApplicationDataDbContext>
    {
        private DbContextOptions<ApplicationDataDbContext> _options;

        public TestAppDataDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<ApplicationDataDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            ApplicationDataDbContext applicationDataDbContext = new ApplicationDataDbContext(_options);
            applicationDataDbContext.Licenses.RemoveRange(applicationDataDbContext.Licenses.ToArray());
            applicationDataDbContext.SaveChanges();
        }

        public ApplicationDataDbContext CreateDbContext()
        {
            return new ApplicationDataDbContext(_options);
        }
    }
}
