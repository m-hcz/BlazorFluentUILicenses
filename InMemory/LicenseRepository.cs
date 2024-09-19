using CoreBusiness;
using UseCases.PluginInterfaces;

namespace InMemory
{
    public class LicenseRepository : ILicenseRepository
    {
        public List<License> licenses;

        public LicenseRepository()
        {
            licenses = new List<License>() {
                new License { Id = 1, ClientEmail = "test1@test.com", CreatedDate = DateTime.UtcNow.AddDays(-10) },
                new License { Id = 2, ClientEmail = "test2@test.com", CreatedDate = DateTime.UtcNow.AddDays(-8) },
                new License { Id = 3, ClientEmail = "test3@test.com", CreatedDate = DateTime.UtcNow.AddDays(-5) },
                new License { Id = 4, ClientEmail = "test4@test.com", CreatedDate = DateTime.UtcNow }
            };
        }

        public Task AddLicenseAsync(License license)
        {
            if (license == null)
                return Task.CompletedTask;

            //if (licenses.Any(_ => _.LicenseName.Equals(license.LicenseName, StringComparison.OrdinalIgnoreCase)))
            //	return Task.CompletedTask;

            if (licenses.Any())
            {
                int maxId = licenses.Max(x => x.Id);
                license.Id = maxId + 1;
            }
            else
                license.Id = 0;

            licenses.Add(license);
            return Task.CompletedTask;
        }

        public Task DeleteLicenseByIdAsync(int id)
        {
            var lic = licenses.FirstOrDefault(_ => _.Id == id);

            if (lic is not null)
                licenses.Remove(lic);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<License>> GetLicensesByNameAsync(string clientEmail)
        {
            if (string.IsNullOrEmpty(clientEmail))
                return await Task.FromResult(licenses);

            return licenses.Where(_ => _.ClientEmail.Contains(clientEmail, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<License?> GetLicenseByIdAsync(int id)
        {
            var lic = licenses.FirstOrDefault(_ => _.Id == id);

            return await Task.FromResult(lic);
        }

        public Task UpdateLicenseAsync(License license)
        {
            if (licenses.Any(_ => _.Id != license.Id && _.ClientEmail.Equals(license.ClientEmail, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var lic = licenses.First(_ => _.Id == license.Id);

            if (lic is not null)
            {
                lic.CreatedDate = license.CreatedDate;
            }

            return Task.CompletedTask;
        }
    }
}
