using OnlineVeterinary.Application;
using OnlineVeterinary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var errorPath = "/error";

{
    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(config);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen();

}

var app = builder.Build();

{
   
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler(errorPath);

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}