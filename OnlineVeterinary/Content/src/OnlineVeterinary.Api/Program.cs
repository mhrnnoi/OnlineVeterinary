using System.Globalization;
using Microsoft.AspNetCore.Localization;
using OnlineVeterinary.Api;
using OnlineVeterinary.Application;
using OnlineVeterinary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

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
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

var app = builder.Build();
{

    app.UseRequestLocalization(options);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(errorPath);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

