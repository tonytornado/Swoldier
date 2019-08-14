using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataRepo;
using System;

namespace OCFX_SPA
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var context = services.GetRequiredService<OCFXContext>();

                    // Create the base classes and such for data.
                    try
                    {
                        InitialLoad.Initialize(context);
                    }
                    catch (Exception exc)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(exc, "Initial loader has failed to seed. Damn you, loader. Maybe you were already okay?? I HOPE YOU WERE, DAMMIT.");
                    }

                    // Create the roles on initialization
                    try
                    {
                        InitialRoles.CreateRoles(serviceProvider, configuration).Wait();
                    }
                    catch (Exception exc)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(exc, "Role creator has failed to seed. Damn you, role creator.");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error has occurred.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
