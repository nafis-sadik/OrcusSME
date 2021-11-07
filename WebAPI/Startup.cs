using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Abstraction;
using Repositories.Implementation;
using UMS.Services.Abstraction;
using Orcus.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orcus.Services.Abstraction;
using UMS.Services.Implementation;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        private const string Dev = "DevCors";
        private const string Prod = "ProductionCors";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: Dev,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                        //builder.WithOrigins("http://localhost:5608/", "http://127.0.0.1:5608/");
                    });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(DataLayer.CommonConstants.PasswordConfig.Salt)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddScoped<IOutletManagerService, OutletManagerService>();
            services.AddScoped(typeof(IOutletManagerService), typeof(OutletManagerService));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

            // Repos
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
            
            services.AddScoped<IUserActivityLogRepo, UserActivityLogRepo>();
            services.AddScoped(typeof(IUserActivityLogRepo), typeof(UserActivityLogRepo));

            services.AddScoped<IEmailIdRepo, EmailIdRepo>();
            services.AddScoped(typeof(IEmailIdRepo), typeof(EmailIdRepo));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
                app.UseCors(Dev);
            }
            else
            {
                app.UseCors(Prod);
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
