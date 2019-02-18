using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Threading.Tasks;

namespace OCFX.Data.DataRepo
{
	public class InitialRoles
	{
		public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
		{
			var _roleManager = serviceProvider.GetRequiredService<RoleManager<OCFXRole>>();
			var _userManager = serviceProvider.GetRequiredService<UserManager<OCFXUser>>();
			string[] RoleNames = { "Administrator", "Coach", "User" };

			foreach (var rolename in RoleNames)
			{
				var x = await _roleManager.RoleExistsAsync(rolename);
				if (!x)
				{
					var role = new OCFXRole
					{
						Name = rolename,
						Description = "Default Role"
					};
					await _roleManager.CreateAsync(role);
				}
			}

			// Wait on the user creation until the site is fixed up.
			//var _user = await _userManager.FindByEmailAsync("tony@phantomhex.com");
			//if (_user == null) { 
			//	var user = new OCFXUser
			//	{
			//		UserName = "tony@phantomhex.com",
			//		Email = "tony@phantomhex.com"
			//	};

			//	string userPWD = "Bankotsu88!";

			//	IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

			//	//Add default User to Role Admin    
			//	if (chkUser.Succeeded)
			//	{
			//	}	var result1 = await _userManager.AddToRoleAsync(user, "Administrator");
			//}
		}
	}
}
