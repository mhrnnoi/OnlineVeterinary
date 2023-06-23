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

var app = builder.Build();
{

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(errorPath);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}