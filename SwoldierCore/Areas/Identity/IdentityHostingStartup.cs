using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SwoldierCore.Areas.Identity.IdentityHostingStartup))]
namespace SwoldierCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}