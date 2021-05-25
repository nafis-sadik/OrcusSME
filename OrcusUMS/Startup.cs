using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services.Abstraction;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcusUMS
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
            services.AddControllersWithViews();

            // Repositories

            services.AddScoped<IActivityTypeRepo, ActivityTypeRepo>();
            services.AddScoped(typeof(IActivityTypeRepo), typeof(ActivityTypeRepo));

            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped(typeof(IAddressRepo), typeof(AddressRepo));

            services.AddScoped<IEmailIdRepo, EmailIdRepo>();
            services.AddScoped(typeof(IEmailIdRepo), typeof(EmailIdRepo));

            services.AddScoped<INumberRepo, NumberRepo>();
            services.AddScoped(typeof(INumberRepo), typeof(NumberRepo));

            services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
            services.AddScoped(typeof(ISubscriptionRepo), typeof(SubscriptionRepo));

            services.AddScoped<ISubscriptionLogRepo, SubscriptionLogRepo>();
            services.AddScoped(typeof(ISubscriptionLogRepo), typeof(SubscriptionLogRepo));

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));

            services.AddScoped<IUserActivityLogRepo, UserActivityLogRepo>();
            services.AddScoped(typeof(IUserActivityLogRepo), typeof(UserActivityLogRepo));

            services.AddScoped<ICrashLogRepo, CrashLogRepo>();
            services.AddScoped(typeof(ICrashLogRepo), typeof(CrashLogRepo));

            // Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddScoped<IUserActivityLogRepo, UserActivityLogRepo>();
            services.AddScoped(typeof(IUserActivityLogRepo), typeof(UserActivityLogRepo));

            // Auth
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CommonConstants.PasswordConfig.Salt)),
                    ValidateIssuer = false,
                    ValidateAudience = false
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
