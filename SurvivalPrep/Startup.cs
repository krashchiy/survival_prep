//Author: Nathan Robbins, Igor Ivanov, Jane Tian
//Date: Dec. 6, 2019
//Course: CS 4540, University of Utah, School of Computing
//Copyright: CS 4540 and Nathan Robbins, Igor Ivanov, Jane Tian - This work may not be copied for use in Academic Coursework.

//We certify that I wrote this code from scratch and did not copy it in part or whole from
//another source. Any references used in the completion of the assignment are cited in my README file.

//File Contents:
//Add services, dependencies and seed question data

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurvivalPrep.DBModels;

namespace SurvivalPrep
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddDbContextPool<PrepContext>(ops =>
                ops.UseSqlServer(Configuration.GetConnectionString("PrepDB")));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            UpdateDatabase(app, env);
        }

        private void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using PrepContext context = scope.ServiceProvider.GetService<PrepContext>();

            //All the seeding code will go inside this clause. It will run only once with db creation.
            //To rerun with any changes, database needs to be dropped first.
            if (context.Database.EnsureCreated() || !context.Questions.Any())
            {
                Utils.ImportTrivia($"{env.ContentRootPath}/Files/Trivia.xlsx", context);
            }
        }
    }
}
