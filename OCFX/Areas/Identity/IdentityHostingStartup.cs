using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

[assembly: HostingStartup(typeof(OCFX.Areas.Identity.IdentityHostingStartup))]
namespace OCFX.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<OCFXContext>(options =>
                    options
                    //.UseInMemoryDatabase("Shard")
                    .UseSqlServer(context.Configuration.GetConnectionString("OCFXContextConnection"))
                    );

                services
                    .AddIdentity<OCFXUser, OCFXRole>()
                    .AddEntityFrameworkStores<OCFXContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            });
        }
    }
}