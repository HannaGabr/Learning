using Jobby.Infra.Persistence.EF;
using Jobby.Infra.Scheduling;
using Jobby.Services.Scheduler.Interfaces;
using Jobby.Infra.CronTool;
using Jobby.Repository.Interfaces;
using Jobby.Services;
using Jobby.Services.Interfaces;
using JobAppProfile = Jobby.Services.JobTracking.Mapping.Profiles.JobProfile;
using JobViewProfile = Jobby.Mapping.Profiles.JobProfile;
using AutoMapper;
using Serilog;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
        }

        public void ConfigureMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new JobAppProfile());
                mc.AddProfile(new JobViewProfile());
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
                .UseSerilogLogProvider()
                .UseMemoryStorage());

            services.AddTransient<IScheduler, SchedulerHangfire>();
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
            services.AddTransient<IJobTrackingService, JobTrackingService>();

            services.AddTransient<ICronHelper, NCronTabHangFHelper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHangfireDashboard();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHangfireServer();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
