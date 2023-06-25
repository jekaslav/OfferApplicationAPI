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
        var connection = Configuration.GetConnectionString("SqlConnection");
        services.AddDbContext<OfferApplicationDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("OfferApplication.API")));
            
        services.AddAutoMapper(typeof(EntityToDtoProfile));
                        
        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IOfferService, OfferService>();  
                        
        services.AddSwaggerGen(c =>
        {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfferApplication.API", Version = "v1" });
        });
    }
        
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfferApplication.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}