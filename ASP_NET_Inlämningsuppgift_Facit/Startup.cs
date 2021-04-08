﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_NET_Inlämningsuppgift_Facit.Data;
using Microsoft.AspNetCore.Identity;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ASP_NET_Inlämningsuppgift_Facit
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
            services.AddRazorPages(o =>
            {
                o.Conventions.AuthorizeFolder("/");
                o.Conventions.AllowAnonymousToFolder("/User");
                // Jag sätter en [AllowAnonymous] direkt i Index sidan för att demonstrera
                //o.Conventions.AllowAnonymousToPage("/Index");

                // Man måste skapa "RequireAdministratorRole" policyn för att detta ska fungera
                // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0#policy-based-role-checks
                // o.Conventions.AuthorizeFolder("/Admin", "RequireAdministratorRole");
            });

            services.AddDbContext<EventDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EventDbContext")));

            services.AddDefaultIdentity<MyUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;

                    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvxyz";
                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedEmail = false;

                    options.Lockout.MaxFailedAccessAttempts = 3;
                })
                .AddEntityFrameworkStores<EventDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                //options.AccessDeniedPath = "/Login";
                options.LoginPath = "/User/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

                options.Cookie.HttpOnly = true;

                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
