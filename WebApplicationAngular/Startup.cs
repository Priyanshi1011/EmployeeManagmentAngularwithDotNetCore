using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationAngular.Models;

namespace WebApplicationAngular
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //var connection = @"server=localhost;port=3306;database=employeedb;user=root;password=Promact2019";
            //services.AddDbContext<EmployeeContext>(options => options.UseMySQL(connection));
            services.AddDbContext<EmployeeContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("TestConnection")));
            //        services.AddDbContext<EmployeeContext>(options =>
            ////options.UseMySQL("Data Source =.; Initial Catalog = Benchmark; Persist Security Info = True; User ID = root; Password = Promact2019"));
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "myapp/dist";
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))
            //    {
            //        context.Request.Path = "/index.html";
            //        context.Response.StatusCode = 200;
            //        await next();
            //    }
            //});
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseMvc(routes =>
            {
 //               routes.MapRoute(
 //               "WithActionApi",
 //               "api/{controller}/{action}/{id}"
 //);
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "myapp";
                
                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");

                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
