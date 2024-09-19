using CoreBusiness;
using FluentAssertions;
using InMemory;
using UseCases.Licenses;

namespace UseCases.Test.Licenses
{
    public class LicenseUseCase_InMemory
    {
        [Fact]
        public async Task LicenseUseCase_AddAsync_Return_True()
        {
            // Arrange
            var newLicense = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.AddAsync(newLicense);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain(newLicense);
        }
        [Fact]
        public async Task LicenseUseCase_AddAsync_Return_Empty()
        {
            // Arrange
            License? newLicense = null;

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.AddAsync(newLicense);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_DeleteAsync_with_valid_id_Return_True()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.DeleteAsync(license.Id);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_DeleteAsync_with_wrong_id_Return_True()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.DeleteAsync(-1);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_EdidtAsync_Return_True()
        {
            // Arrange
            var editedTime = DateTime.Now.AddDays(5);
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var editedlicense = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = editedTime };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.EdidtAsync(editedlicense);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().NotBeNullOrEmpty();
            result[0].CreatedDate.Should().Be(editedTime);
        }
        [Fact]
        public async Task LicenseUseCase_EdidtAsync_Witn_Wrong_Id_Return_Prevous_Data()
        {
            // Arrange
            var editedTime = DateTime.Now.AddDays(5);
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var editedlicense = new CoreBusiness.License {Id = -1, ClientEmail = "newtest@test.com", CreatedDate = editedTime };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            await licenseUseCase.EdidtAsync(editedlicense);
            var result = inMemoryRepo.licenses;

            // Assert
            result.Should().NotBeNullOrEmpty();
            result[0].CreatedDate.Should().Be(license.CreatedDate);
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_Return_Correct_Data()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);
            inMemoryRepo.licenses.Add(license2);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            var result = await licenseUseCase.ViewLicencesByNameAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_By_ClientEmail_Return_Correct_Data()
        {
            // Arrange
            string email = "newtest2@test.com";

            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { ClientEmail = email, CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);
            inMemoryRepo.licenses.Add(license2);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            var result = await licenseUseCase.ViewLicencesByNameAsync(email);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.First().ClientEmail.Should().Be(email);
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_With_Wrong_ClientEmail_Return_Correct_Data()
        {
            // Arrange
            string email = "newtest3@test.com";

            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);
            inMemoryRepo.licenses.Add(license2);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            var result = await licenseUseCase.ViewLicencesByNameAsync(email);

            // Assert
            result.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicenseByIdUseCaseAsync_By_ClientEmail_Return_Correct_Data()
        {
            // Arrange
            int id = 2;
            var license = new CoreBusiness.License { Id = 1, ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { Id = id, ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);
            inMemoryRepo.licenses.Add(license2);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            var result = await licenseUseCase.ViewLicenseByIdUseCaseAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_With_Wrong_Id_Return_Correct_Data()
        {
            // Arrange
            int id = -1;
            var license = new CoreBusiness.License { Id = 1, ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { Id = 2, ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var inMemoryRepo = new LicenseRepository();
            inMemoryRepo.licenses.Clear();
            inMemoryRepo.licenses.Add(license);
            inMemoryRepo.licenses.Add(license2);

            var licenseUseCase = new LicenseUseCase(inMemoryRepo);

            // Act
            var result = await licenseUseCase.ViewLicenseByIdUseCaseAsync(id);

            // Assert
            result.Should().BeNull();
        }
    }
}