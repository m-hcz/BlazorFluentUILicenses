using CoreBusiness;

namespace UseCases.PluginInterfaces
{
	public interface ILicenseRepository
	{
		Task AddLicenseAsync(License license);
		Task DeleteLicenseByIdAsync(int id);
		Task<IEnumerable<License>> GetLicensesByNameAsync(string clientEmail);
		Task<License?> GetLicenseByIdAsync(int id);
		Task UpdateLicenseAsync(License license);
	}
}