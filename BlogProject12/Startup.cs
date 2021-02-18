using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository;
using BlogProject12.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BlogProject12.Utilities;
using Stripe;
using BlogProject12.Utilities.CashfreePayment;
using Microsoft.AspNetCore.DataProtection;


namespace BlogProject12
{
    public class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<CashFreeKeys>(Configuration.GetSection("CashFree"));
            
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/facebook-login";
            }
            ).AddFacebook(options =>
            {
                options.AppId = "239544987891039";
                options.AppSecret = "30831d578b18193a233269290c91feab";
            });

            /*services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60)+;*/



       /*     services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/User/User/UserLogin");
                options.AccessDeniedPath = new PathString("/User/User/UserSignUp");
            });
*/
            IMvcBuilder builder = services.AddControllersWithViews();
            // if (Env.IsDevelopment())
            //{
            builder.AddRazorRuntimeCompilation();
            // }

          /* services.AddAuthentication().AddFacebook(options => { options.AppId = "239544987891039"; options.AppSecret = "30831d578b18193a233269290c91feab"; });*/


        }

        //partial class

    

           
           /* public void ConfigureAuth(IAppBuilder app)
            {
                app.UseFacebookAuthentication(
                              appId: "239544987891039",
                              appSecret: "30831d578b18193a233269290c91feab");
            }*/
        

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=User}/{controller=User}/{action=UserLogin}/{id?}");
            });
        }
    }
}
