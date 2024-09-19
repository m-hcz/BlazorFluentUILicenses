using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityEFCore.Data
{
	public class ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
	}
}
