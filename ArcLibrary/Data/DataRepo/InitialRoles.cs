using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Threading.Tasks;

namespace OCFX.Data.DataRepo
{
    public static class InitialRoles
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<OCFXRole>>();
            serviceProvider.GetRequiredService<UserManager<OCFXUser>>();
            string[] roleNames = {"Administrator", "Coach", "User"};

            foreach (var roleName in roleNames)
            {
                var x = await roleManager.RoleExistsAsync(roleName);
                if (x) continue;
                var role = new OCFXRole
                {
                    Name = roleName,
                    Description = "Default Role"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
