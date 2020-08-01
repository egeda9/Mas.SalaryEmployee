using Mas.SalaryEmployee.DataAccess;
using Mas.SalaryEmployee.DataAccess.Implementation;
using Mas.SalaryEmployee.Model.Dto;
using Mas.SalaryEmployee.Services;
using Mas.SalaryEmployee.Services.Factory;
using Mas.SalaryEmployee.Services.Factory.Implementation;
using Mas.SalaryEmployee.Services.Implementation;
using Mas.SalaryEmployee.Util;
using Mas.SalaryEmployee.Util.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mas.SalaryEmployee.Api
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
            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddControllers();

            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<HourlyBasedSalary>();
            services.AddScoped<MonthlyBasedSalary>();
            services.AddScoped<IContractFactory, ContractFactory>();

            services.AddScoped<IEmployeeData, EmployeeData>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Salary Employee API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
