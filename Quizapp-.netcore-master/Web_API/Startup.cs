using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;
using Web_API.Helpers;
using Web_API.Models;
using Web_API.Services;

namespace Web_API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(
                c => c.AddPolicy("QuizAppPolicy", options =>
                {
                        options.WithOrigins("http://localhost:4200")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader();
                }));

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = Configuration["QuizAppAuthority"];
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "QuizApp.WebApi";
                        options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Jwt;
                    });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IUserTypeService, UserTypeService>();

            services.AddDbContext<Quiz_DBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
           
                app.UseCors(x => x.WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                ) ;

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
