using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfferApplication.Domain.Contexts;
using OfferApplication.Services.Interfaces;
using OfferApplication.Services.Mappers;
using OfferApplication.Services.Services;

namespace OfferApplication.API;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfferApplication.API", Version = "v1" });
        });
        
        var connection = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddDbContext<OfferApplicationDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("OfferApplication.API")));
            
        builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
                        
        builder.Services.AddScoped<IProviderService, ProviderService>();
        builder.Services.AddScoped<IOfferService, OfferService>();  
    }
        
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfferApplication.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
    }
}