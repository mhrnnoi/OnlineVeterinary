using OnlineVeterinary.Application;
using OnlineVeterinary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

}

var app = builder.Build();

{
   
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}