using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<OCFXContext>(options =>
                    options
                    //.UseSqlite("Data Source=OCFX.db")
                    .UseInMemoryDatabase("Shard")
                    //.UseSqlServer(context.Configuration.GetConnectionString("OCFXContextConnection"))
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