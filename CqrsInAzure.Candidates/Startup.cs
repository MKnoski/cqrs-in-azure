﻿using CqrsInAzure.Candidates.EventGrid.Publishers;
using CqrsInAzure.Candidates.Repositories;
using CqrsInAzure.Candidates.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CqrsInAzure.Candidates
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
            RegisterServices(services);
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CqrsInAzure.Candidates", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "docs";
                });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICandidatesRepository, CandidatesRepository>();
            services.AddSingleton<IRequestRepository, RequestRepository>();
            services.AddSingleton<ICvStorage, CvStorage>();
            services.AddSingleton<IPhotosStorage, PhotosStorage>();
            services.AddSingleton<ICandidateEventPublisher, CandidateEventPublisher>();
        }
    }
}