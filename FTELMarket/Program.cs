namespace FTELMarket;

using FTELMarket.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<FtelMarketDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("FtelMarket")));

        // Configuration CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontEnd", policy =>
            {
                policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // 1. Activer CORS
        app.UseCors("AllowFrontEnd");

        // 2. DESACTIVER la redirection HTTPS pour autoriser HTTP sur le port 5082
        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}