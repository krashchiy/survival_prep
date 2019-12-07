//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//We certify that I wrote this code from scratch and did not copy it in part or whole from
//another source. Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//Application entry point. Seed users/items/disasters info on startup

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SurvivalPrep.DBModels;
using Microsoft.AspNetCore.Identity;

namespace SurvivalPrep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //SeedData.Initialize(services);
                    var usersRolesDB = services.GetRequiredService<PrepContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    UserDBInitializer.Initialize(usersRolesDB,userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
