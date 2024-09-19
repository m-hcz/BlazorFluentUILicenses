using DataEFCore;
using FluentAssertions;
using UseCases.Licenses;

namespace UseCases.Test.Licenses
{
    public class LicenseUseCase_EFCore
    {
        [Fact]
        public async Task LicenseUseCase_AddAsync_Return_True()
        {
            // Arrange
            var newLicense = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();
            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.AddAsync(newLicense);

            // Assert
            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Should().NotBeNullOrEmpty();
                db.Licenses.Should().HaveCount(1);
            }
        }
        [Fact]
        public async Task LicenseUseCase_AddAsync_With_Null_Return_Empty()
        {
            // Arrange
            var dbContext = new TestAppDataDbContextFactory();
            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.AddAsync(null);

            // Assert
            using (var db = dbContext.CreateDbContext())
                db.Licenses.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_DeleteAsync_with_valid_id_Return_True()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.DeleteAsync(license.Id);

            // Assert

            using (var db = dbContext.CreateDbContext())
                db.Licenses.Should().BeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_DeleteAsync_with_wrong_id_Return_True()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.DeleteAsync(-1);

            // Assert
            using (var db = dbContext.CreateDbContext())
                db.Licenses.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task LicenseUseCase_EdidtAsync_Return_True()
        {
            // Arrange
            var editedTime = DateTime.Now.AddDays(5);
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var editedlicense = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = editedTime };

            // Arrange
            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(editedlicense);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.EdidtAsync(editedlicense);

            // Assert
            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Should().NotBeNullOrEmpty();
                db.Licenses.First().CreatedDate.Should().Be(editedTime);
            }
        }
        [Fact]
        public async Task LicenseUseCase_EdidtAsync_Witn_Wrong_Id_Return_Prevous_Data()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var wrongLic = new CoreBusiness.License { Id = -1, ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now.AddDays(5) };

            // Arrange
            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            await licenseUseCase.EdidtAsync(wrongLic);

            // Assert
            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Should().NotBeNullOrEmpty();
                db.Licenses.First().CreatedDate.Should().Be(license.CreatedDate);
            }
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_Return_Correct_Data()
        {
            // Arrange
            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(license2);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            var result = await licenseUseCase.ViewLicencesByNameAsync();

            // Assert
            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Should().NotBeNullOrEmpty();
                db.Licenses.Should().HaveCount(2);
            }
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_By_ClientEmail_Return_Correct_Data()
        {
            // Arrange
            string email = "newtest2@test.com";

            var license = new CoreBusiness.License { ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { ClientEmail = email, CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(license2);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

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

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(license2);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

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

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(license2);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            var result = await licenseUseCase.ViewLicenseByIdUseCaseAsync(id);

            // Assert
            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Should().NotBeNull();
                db.Licenses.First().Id.Should().Be(id);
            }
        }
        [Fact]
        public async Task LicenseUseCase_ViewLicencesByNameAsync_With_Wrong_Id_Return_Correct_Data()
        {
            // Arrange
            int id = -1;
            var license = new CoreBusiness.License { Id = 1, ClientEmail = "newtest@test.com", CreatedDate = DateTime.Now };
            var license2 = new CoreBusiness.License { Id = 2, ClientEmail = "newtest2@test.com", CreatedDate = DateTime.Now };

            var dbContext = new TestAppDataDbContextFactory();

            using (var db = dbContext.CreateDbContext())
            {
                db.Licenses.Add(license);
                db.Licenses.Add(license2);
                await db.SaveChangesAsync();
            }

            var licenseEFCoreRepository = new LicenseEFCoreRepository(dbContext);

            var licenseUseCase = new LicenseUseCase(licenseEFCoreRepository);

            // Act
            var result = await licenseUseCase.ViewLicenseByIdUseCaseAsync(id);

            // Assert
            result.Should().BeNull();
        }
    }
}
