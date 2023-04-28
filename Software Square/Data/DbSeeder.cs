using Microsoft.AspNetCore.Identity;
using Software_Square.Constants;

namespace Software_Square.Data
{
	public static class DbSeeder
	{
		public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
		{
			//Seed Roles
			var userManager = service.GetService<UserManager<ApplicationUser>>();
			var roleManager = service.GetService<RoleManager<IdentityRole>>();
			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));

			// creating admin

			var user = new ApplicationUser
			{
				UserName = "miznarauf19@gmail.com",
				Email = "miznarauf19@gmail.com",
				FirstName = "Mizna",
				LastName = "Rauf",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				contact = "+92-300-000-0000"
			};
			var userInDb = await userManager.FindByEmailAsync(user.Email);
			if (userInDb == null)
			{
				await userManager.CreateAsync(user, "Admin@123");
				await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
			}
		}
	}
}
