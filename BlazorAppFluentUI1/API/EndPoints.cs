using UseCases.Licenses;

namespace BlazorAppFluentUI1.API
{
    public static class EndPoints
    {
        public static void MapEndPoints(this WebApplication app)
        {
            var licenses = app.MapGroup("/api/v1");

            licenses.MapGet("/licenses/{email}", (string email, ILicenseUseCase LicenseUseCase) =>
            {
                return LicenseUseCase.ViewLicencesByNameAsync(email);
            });
        }
    }
}
