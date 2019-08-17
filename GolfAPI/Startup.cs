using System;
using GolfAPI.Core.Contracts;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
using GolfAPI.Core.Managers;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GolfAPI
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
            services.AddCors(
                o => o.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //database config
            var connection = Configuration.GetConnectionString("GolfTestConnection");
            services.AddDbContext<GolfDatabaseContext>
                (options => {
                    options.UseSqlServer(connection);
             
                });
            // create repository service
            services.AddScoped<BaseRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();

            //create managers
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IComponentOrderManager, ComponentOrderManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        }
    }
}
