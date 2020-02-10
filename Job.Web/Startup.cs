using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Jobby.Infra.Mapping.AutoMap.Profiles;
using Jobby.Infra.Persistence.EF;
using Jobby.Repository.Interfaces;
using Jobby.Services;
using Jobby.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Jobby
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
            ConfigureMvc(services);
            ConfigureMapper(services);
            ConfigureScheduler(services);
            ConfigureDatabase(services);
            ConfigureRepositories(services);
            ConfigureCustomServices(services);
        }

        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new JobProfile());
                mc.AddProfile(new JobInstanceProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
        }

        private void ConfigureScheduler(IServiceCollection services)
        {
            //TODO redis
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            //TODO postgres
            services.AddDbContext<JobTrackingContext>(opt => opt.UseInMemoryDatabase(databaseName: "JobTracking"));
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IJobRepository, JobRepositoryEF>();
            services.AddTransient<IJobInstanceRepository, JobInstanceRepositoryEF>();
            services.AddTransient<IUnitOfWork, UnitOfWorkEF>();
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddTransient<IJobService, JobService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHangfireServer();
                app.UseHangfireDashboard();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
