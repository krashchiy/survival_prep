using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurvivalPrep.DBModels;
using SurvivalPrep.Models;

[assembly: HostingStartup(typeof(SurvivalPrep.Areas.Identity.IdentityHostingStartup))]
namespace SurvivalPrep.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<PrepContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("PrepDB")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<PrepContext>();
            });
        }
    }
}