using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizApp.IdentityServer.QuizAppIdentity;
using Web_API.Models;

namespace QuizApp.IdentityServer
{
    public class Startup
    {
        IHostEnvironment environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IUserStore<QuizUser>, QuizUserStore>();
            services.AddTransient<IRoleStore<QuizUserRole>, QuizUserRoleStore>();
            services.AddDbContext<Quiz_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddCors(o => o.AddPolicy("QuizAppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddIdentity<QuizUser, QuizUserRole>()
                    .AddUserStore<QuizUserStore>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddAspNetIdentity<QuizUser>()
                .AddProfileService<QuizAppProfileService>();

            services.AddAuthentication();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("QuizAppPolicy");

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();

        }
    }
}
