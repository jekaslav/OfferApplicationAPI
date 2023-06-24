using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfferApplication.Domain.Contexts;
using OfferApplication.Services.Interfaces;
using OfferApplication.Services.Mappers;
using OfferApplication.Services.Services;

namespace OfferApplication;

public class Startup
{
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToDtoProfile));
            
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IProviderService, ProviderService>();
            

            var connection = Configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<OfferApplicationContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("OfferApplication.API")));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationSystem.API", Version = "v1" });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReservationSystem.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }