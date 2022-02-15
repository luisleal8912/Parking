using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api
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

            services.AddControllers();
            services.AddScoped<Data.Access.Implementation.IDiscount, Data.Access.Implementation.Discount>();
            services.AddScoped<Data.Access.Implementation.IEntry, Data.Access.Implementation.Entry>();
            services.AddScoped<Data.Access.Implementation.IPayment, Data.Access.Implementation.Payment>();
            services.AddScoped<Data.Access.Implementation.ITypeVehicle, Data.Access.Implementation.TypeVehicle>();
            services.AddScoped<Data.Access.Implementation.IVehicle, Data.Access.Implementation.Vehicle>();


            services.AddScoped<Business.Rules.Implementation.IDiscount, Business.Rules.Implementation.Discount>();
            services.AddScoped<Business.Rules.Implementation.IEntry, Business.Rules.Implementation.Entry>();
            services.AddScoped<Business.Rules.Implementation.IPayment, Business.Rules.Implementation.Payment>();
            services.AddScoped<Business.Rules.Implementation.ITypeVehicle, Business.Rules.Implementation.TypeVehicle>();
            services.AddScoped<Business.Rules.Implementation.IVehicle, Business.Rules.Implementation.Vehicle>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking.Api v1"));
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
