using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using WebApiTeste.Application.Services;
using WebApiTeste.Application.Services.Interfaces;
using WebApiTeste.Domain.Repositories;
using WebApiTeste.Infrastructure.Context;
using WebApiTeste.Infrastructure.Repositories;

namespace WebApiTeste
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiTeste", Version = "v1" });
            });

            services.AddDbContext<DatabaseContext>(opt =>
            {
                var server = Environment.GetEnvironmentVariable("DATABASE_SERVER");
                var database = Environment.GetEnvironmentVariable("DATABASE_NAME");
                var login = Environment.GetEnvironmentVariable("DATABASE_LOGIN");
                var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

                var connectionString = $"Server={server};Database={database};User Id={login};Password={password};MultipleActiveResultSets=True;";

                opt.UseSqlServer(connectionString);
            });

            services.AddHealthChecks();

            services.AddScoped<ITesteService, TesteService>();
            services.AddScoped<ITesteRepository, TesteRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiTeste v1"));

            app.UseHealthChecks("/healthcheck");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
