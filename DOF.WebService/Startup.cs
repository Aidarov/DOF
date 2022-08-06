using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOF.WebService.Database;
using DOF.WebService.Middlewares;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.Device.DevicePath;
using DOF.WebService.Services.Measure;
using DOF.WebService.Services.OilField;
using DOF.WebService.Services.OilWell;
using DOF.WebService.Services.TabletUser;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DOF.WebService
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
            services.AddDbContext<MainDbContext>(options => options.UseNpgsql(Configuration["ConnectionStrings:PostgreSQL"]));

            services.AddScoped<IUserSessionService, UserSessionService>();
            services.AddScoped<IUserService, UserService>();            
            services.AddScoped<ITabletUserService, TabletUserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDevicePathService, DevicePathService>();
            services.AddScoped<IOilFieldService, OilFieldService>();
            services.AddScoped<IOilWellService, OilWellService>();
            services.AddScoped<IMeasureService, MeasureService>();
            

            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseMiddleware<AuthorizeMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
