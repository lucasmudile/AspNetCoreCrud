using FN.Store.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FN.StoreApi
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
            services.AddMemoryCache();
            services.AddMvc().
                AddJsonOptions(options=> {
              //  options.SerializerSettings.ReferenceLoopHandlig = Newtonsoft.Microsoft.ReferenceLoopHandlig.Ignore();
                //options.JsonSerializerOptions.N;    
            });

            services.AddDependencies();
            services.AddControllers();
           
            services.AddSwaggerGen(s =>
           {
               s.SwaggerDoc("v1", new  OpenApiInfo
               { 
                   Title = "Store API", 
                   Version = "v1",
                   Description="Api para gestão de Stock",
                   TermsOfService=new System.Uri("https://abcxyz.com"),
                   Contact=new OpenApiContact()
                   {
                       Name="Lucas Santana",
                       Email="lucas@gmail.com",
                       Url=new System.Uri("https://abcxyz.com")
                   },
                   License=new OpenApiLicense()
                   {
                       Name="Open Source",
                       Url=new System.Uri("https://opensource.com")
                   }
               });
           });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s=> {
                s.SwaggerEndpoint("swagger/v1/swagger.json", "Produtos API");
                s.RoutePrefix = string.Empty;
            });

          
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
