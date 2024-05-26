using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Data;

namespace CrudProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Bu metod, servisleri yapılandırır.
        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = Configuration.GetConnectionString("MyDatabase");
            services.AddSingleton(connectionString);

            services.AddScoped<Common.DbHelper>();
            services.AddControllersWithViews();
            services.AddCors();
            services.AddControllers();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crud Project Swagger", Version = "v1" });
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
            });

       
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Project Swagger");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // Content klasörünün yolunu belirle
            var contentPath = Path.Combine(Directory.GetCurrentDirectory(), "Content");

            // Eğer Content klasörü yoksa oluştur
            if (!Directory.Exists(contentPath))
            {
                Directory.CreateDirectory(contentPath);
            }

            app.UseHttpsRedirection();
            // Statik dosyaları kullanmak için yapılandırma
            app.UseStaticFiles(); // Varsayılan wwwroot için
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(contentPath),
                RequestPath = "/Content"
            });

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Project Swagger");
            });

            app.UseRouting();

            app.UseCors("foo"); // second

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseAuthorization();

            /* ###
           * Upload işlemleri için statik dosya sistemi.
           * ------------------------------------------------------------------------------------------------- */
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Upload")))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
                RequestPath = new PathString("/" + "Upload")
            });
            /* $$$ */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
