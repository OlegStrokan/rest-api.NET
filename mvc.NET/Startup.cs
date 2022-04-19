using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using mvc.NET.Repositories;
using mvc.NET.Settings;

namespace mvc.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // подключения env переменных
        public IConfiguration Configuration { get; }


        // подключения всех сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            
            // для правильного взаимодействия с guid и date
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(mongoDbSettings.ConnectionString);
            });
            services.AddControllersWithViews();
            
            // интерфейс сервиса и сам сервис
            services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

            services.AddControllers(option =>
            {
                option.SuppressAsyncSuffixInActionNames = false;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Catalog", Version = "v0.0.1"});
            });

            services.AddHealthChecks()
                // если мы не можем подключиться к базе данных в течении 3х секунд, endpoint health будет выдавать ошибку
                .AddMongoDb(
                    mongoDbSettings.ConnectionString,
                    name: "mongodb",
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] { "ready", "live" });
        }
        
        // pipeline - подключения доп функционала - jwt, swagger, static файлы
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection()
                ;
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains("ready")
                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                   Predicate = (_) => false
                });
            });
        }
    }
}
