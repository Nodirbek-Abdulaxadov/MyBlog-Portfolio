using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog_Portfolio.Areas.Identity.Data;
using MyBlog_Portfolio.Data;

[assembly: HostingStartup(typeof(MyBlog_Portfolio.Areas.Identity.IdentityHostingStartup))]
namespace MyBlog_Portfolio.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyBlog_PortfolioContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("PostgreDB")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MyBlog_PortfolioContext>();
            });
        }
    }
}