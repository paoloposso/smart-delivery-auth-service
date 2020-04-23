using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartDelivery.Auth.CrossCutting.DI;
using Microsoft.OpenApi.Models;

namespace SmartDelivery.Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Console.WriteLine($"Database: {Configuration.GetSection("ConnectionStrings").GetValue<string>("Mongo")}");
            Console.WriteLine($"Secret: {Configuration.GetSection("JwtConfig").GetValue<string>("Secret")}");

            AppSettings = new AppSettings
            {
                MongoDbCnnString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Mongo"),
                AllowedHosts = Configuration.GetValue<string>("AllowedHosts"),
                JwtExpirationTimeInMinutes = Configuration.GetSection("JwtConfig").GetValue<int>("TokenExpirationInMinutes"),
                JwtSecret = Configuration.GetSection("JwtConfig").GetValue<string>("Secret")
            };
        }

        public IConfiguration Configuration { get; }
        private AppSettings AppSettings { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            new DependencyInjectionContainer(AppSettings).Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
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
