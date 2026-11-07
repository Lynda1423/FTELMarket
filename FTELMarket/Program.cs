
namespace FTELMarket;

using FTELMarket.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FTELMarket.Settings;
using FTELMarket.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        builder.Services.AddOptions<FlakySettings>()
            .BindConfiguration(nameof(FlakySettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // 1. Liaison et validation des options FlakySettings
        builder.Services.AddOptions<FlakySettings>()
            .BindConfiguration("FlakySettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // 2. Client typé (Typed Client) pour FlakyService
        builder.Services.AddHttpClient<FlakyService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<FlakySettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseAddress);
        });

        // 3. Client nommé (Named Client "Flaky")
        builder.Services.AddHttpClient("Flaky", (sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<FlakySettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseAddress);
        });
        
        
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer(); 
        builder.Services.AddSwaggerGen();           

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();   
            app.UseSwaggerUI();
}

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