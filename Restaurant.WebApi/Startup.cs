using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Spi;
using Restaurant.WebApi.DependencyInjection;
using Restaurant.WebApi.Infrastructure.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant.WebApi", Version = "v1" });
            });

            services.ApplicationSecurityCollectionConfiguration(Configuration);
            services.ConfigureRepositoryCollection();
            services.ConfigureServiceCollection();

            //services.AddSingleton<IJobFactory, SingletonJobFactory>();
            //services.AddSingleton<ImportOLTPDataToDWJob>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                // Create a "key" for the job
                var jobKey = new JobKey("ImportOLTPDataToDWJob");

                // Register the job with the DI container
                q.AddJob<ImportOLTPDataToDWJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the HelloWorldJob
                    .WithIdentity("ImportOLTPDataToDWJob-trigger") // give the trigger a unique name
                    .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(3)
                    .RepeatForever()));
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant.WebApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
