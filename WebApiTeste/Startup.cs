using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
                var server = Configuration.GetConnectionString("DATABASE_SERVER");
                var database = Configuration.GetConnectionString("DATABASE_NAME");
                var login = Configuration.GetConnectionString("DATABASE_LOGIN");
                var password = Configuration.GetConnectionString("DATABASE_PASSWORD");

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
