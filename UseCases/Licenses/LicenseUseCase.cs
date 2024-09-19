using CoreBusiness;
using UseCases.PluginInterfaces;

namespace UseCases.Licenses
{
	public class LicenseUseCase(ILicenseRepository licenseRepository) : ILicenseUseCase
	{
		public async Task AddAsync(License license)
		{
			await licenseRepository.AddLicenseAsync(license);
		}

		public async Task DeleteAsync(int id)
		{
			await licenseRepository.DeleteLicenseByIdAsync(id);
		}
		public async Task EdidtAsync(License license)
		{
			await licenseRepository.UpdateLicenseAsync(license);
		}

		public async Task<IEnumerable<License>> ViewLicencesByNameAsync(string name = "")
		{
			return await licenseRepository.GetLicensesByNameAsync(name);
		}

		public async Task<License?> ViewLicenseByIdUseCaseAsync(int id)
		{
			return await licenseRepository.GetLicenseByIdAsync(id);
		}
	}
}
