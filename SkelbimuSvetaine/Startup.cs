using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SkelbimuSvetaine
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
            services.AddSession();
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddDbContext<ld1_gynimasContext>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                    options.Events = new CookieAuthenticationEvents()
                    {
                         OnSigningIn = async context =>
                         {
                             var principal = context.Principal;
                             if(principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                             {
                                 if(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "admin")
                                 {
                                     var claimsIdentity = principal.Identity as ClaimsIdentity;
                                     claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                                 }
                             }
                             await Task.CompletedTask;
                         },
                        OnSignedIn = async context =>
                        {
                            await Task.CompletedTask;
                        },
                        OnValidatePrincipal = async context =>
                        {
                            await Task.CompletedTask;
                        }
                    };
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
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
            //app.UseStaticFiles();

            //app.UseMvc(routes => { routes.MapRoute(name: "default", template: "") })
        }
    }
}
