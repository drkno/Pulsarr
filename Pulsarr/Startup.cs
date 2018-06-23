using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pulsarr.Download;
using Pulsarr.Library;
using Pulsarr.Metadata;
using Pulsarr.PostDownload;
using Pulsarr.Preferences;
using Pulsarr.Search;
using Swashbuckle.AspNetCore.Swagger;

// ReSharper disable UnusedMember.Global
namespace Pulsarr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddMvc();
            mvc.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            
            mvc.AddApplicationPart(PreferencesServiceRegistry.ConfigureServices(services));
            mvc.AddApplicationPart(DownloadServiceRegistry.ConfigureServices(services));
            mvc.AddApplicationPart(MetadataServiceRegistry.ConfigureServices(services));
            mvc.AddApplicationPart(LibraryServiceRegistry.ConfigureServices(services));
            mvc.AddApplicationPart(PostDownloadServiceRegistry.ConfigureServices(services));
            mvc.AddApplicationPart(SearchServiceRegistry.ConfigureServices(services));

            mvc.AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Pulsarr API", Version = "v1" });
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

            app.UseSwagger(c => c.RouteTemplate = "/api/swagger-{documentName}.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger-v1.json", "Pulsarr API");
                c.RoutePrefix = "api";
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "WebUI";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });

//            app.UseSignalR(routes =>
//            {
//                routes.
//            })
        }
    }
}
