using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Api;
using OnlineVeterinary.Application;
using OnlineVeterinary.Infrastructure;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

{
    builder.Services.AddApplication()
                    .AddInfrastructure(config)
                    .AddPresentation();
}

var errorPath = "/error";

var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR")
    };

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};


var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
    app.UseRequestLocalization(options);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(errorPath);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

