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
            if (Configuration == null)
            {
                throw new ArgumentNullException(nameof(Configuration));
            }

            RoleManager<OCFXRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<OCFXRole>>();
            serviceProvider.GetRequiredService<UserManager<OCFXUser>>();
            string[] RoleNames = { "Administrator", "Coach", "User" };

            foreach (string rolename in RoleNames)
            {
                bool x = await _roleManager.RoleExistsAsync(rolename);
                if (!x)
                {
                    OCFXRole role = new OCFXRole
                    {
                        Name = rolename,
                        Description = "Default Role"
                    };
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
