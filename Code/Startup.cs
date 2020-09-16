using API;
using API.Models;
using API.Services;
using Hackathon.CustomMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Security.Claims;

namespace AspNetCoreApps
{
    public class Startup
    {
        /// <summary>
        /// IConfiguration, the contract, that provides application configuration information to
        /// the Startup class. All these configurations are written in appsettings.json
        /// e.g. ConnectionStrings
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// The method for defining required services (congfiguration and Dependencies) in the container
        /// IServiceCollection is used to register services in the container and the lifetime of service types
        /// is managed by the 'ServiceDescriptor' class
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // regiter the _DbContext in the Container
            services.AddDbContext<_DbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppConnString")));

            // register the service classes

            services.AddScoped<IService<CompetencyFramework, int>, CompetencyFrameworkService>();
            services.AddScoped<IService<CompetencyDetail, int>, CompetencyDetailService>();
            services.AddScoped<ICompetencyDetail, CompetencyDetailService>();
            services.AddScoped<IService<EmployeeCompetency, int>, EmployeeCompetencyService>();
            services.AddScoped<IEmployeeCompetency, EmployeeCompetencyService>();
            services.AddScoped<IService<Information, int>, InformationService>();

            services.AddCors(options => options.AddPolicy("corspolicy", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-7z2owzae.us.auth0.com/";
                options.Audience = "30285";
                //options.Authority = domain;
                //options.Audience = Configuration["Auth0:ApiIdentifier"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            });
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        ///  This method represent the current Http Request and all additional objects to be provided
        ///  the Http request e.g. Security, Exception., etc
        ///  IApplicationBuilder: Builds all "middleware"  for current Http Request
        ///  IHostingEnvironment: provided Hosting
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                           builder
                             .WithOrigins("http://localhost:4200")
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials()
                         );

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            // register the custom middleware
            app.UseAuthentication();
            app.UseCustomErrorHandlerMiddleware();
            app.UseMvc();
        }
    }
}
