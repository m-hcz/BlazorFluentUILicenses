using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.PluginInterfaces;

namespace DataEFCore
{
    public class LicenseEFCoreRepository(IDbContextFactory<ApplicationDataDbContext> contextFactory) : ILicenseRepository
    {
        public async Task AddLicenseAsync(License license)
        {
            if (license != null)
            {
                using var db = contextFactory.CreateDbContext();
                db.Licenses?.Add(license);

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteLicenseByIdAsync(int id)
        {
            using var db = contextFactory.CreateDbContext();

            var license = db.Licenses?.Find(id);

            if (license == null) return;

            db.Licenses?.Remove(license);

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<License>> GetLicensesByNameAsync(string name)
        {
            using var db = contextFactory.CreateDbContext();
            return await db.Licenses.Where(_ => _.ClientEmail.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task<License?> GetLicenseByIdAsync(int id)
        {
            using var db = contextFactory.CreateDbContext();
            var license = await db.Licenses.FindAsync(id);

            return license;
        }

        public async Task UpdateLicenseAsync(License license)
        {
            using var db = contextFactory.CreateDbContext();
            var lic = await db.Licenses.FindAsync(license.Id);

            if (lic is not null)
            {
                lic.CreatedDate = license.CreatedDate;

                await db.SaveChangesAsync();
            }
        }
    }
}
