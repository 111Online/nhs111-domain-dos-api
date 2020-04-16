using System.ServiceModel;
using AutoMapper;
using DirectoryOfServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NHS111.Domain.Dos.Api.Mappers;
using NHS111.Domain.Dos.Api.Services;

namespace NHS111.Domain.Dos.Api
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
            services.AddApplicationInsightsTelemetry();
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IPathwayServiceSoapFactory>(new PathwayServiceSoapFactory(Configuration));
            services.AddSingleton<IMonitorService>(new MonitorService(new PathwayServiceSoapFactory(Configuration), Configuration));
            services.AddScoped<ApiExceptionFilter>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddLogging(log =>
                log.AddLog4Net(Configuration.GetValue<string>("Logging:Log4NetConfigFile:Name"))
            );
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
