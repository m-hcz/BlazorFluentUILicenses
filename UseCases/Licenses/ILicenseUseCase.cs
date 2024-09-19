using CoreBusiness;

namespace UseCases.Licenses
{
	public interface ILicenseUseCase
	{
		Task AddAsync(License license);
		Task DeleteAsync(int id);
		Task EdidtAsync(License license);
		Task<IEnumerable<License>> ViewLicencesByNameAsync(string name = "");
		Task<License?> ViewLicenseByIdUseCaseAsync(int id);
	}
}