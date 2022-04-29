using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task20.Models;

namespace Task20
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(opts => {
                    opts.User.RequireUniqueEmail = true;    // ���������� email
                    //opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // ���������� �������
                    opts.Password.RequiredLength = 5;   // ����������� �����
                    opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                    opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                    opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                    opts.Password.RequireDigit = false; // ��������� �� �����
                })
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddMvc(); ;
            services.AddTransient<ApplicationContext>();
            services.AddControllersWithViews();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
