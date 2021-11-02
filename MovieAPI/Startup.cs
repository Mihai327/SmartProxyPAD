﻿

using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieAPI.Repositories;
using MovieAPI.Settings;

namespace WebApplication3
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
            //configurarea MongoDbSettings pentru a putea fi injectate
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDbSettings>(provider => provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            //configurarea IMongoRepository pentru a putea fi injectat
            services.AddScoped<IMongoRepository<Movie>, MongoRepository<Movie>>();
            services.AddControllers();
            //adăugăm serviciile Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //trebuie sa adaugam swagger middleware-ul la procesarea pipeline-ului cererilor HTTP
            app.UseSwagger();
            // SweggerUI acceseaza documentul json respectiv si genereaza automat documentatia despre proiectul MovieAPI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
