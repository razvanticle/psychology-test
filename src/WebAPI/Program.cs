using Application;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence;
using WebAPI.ExceptionHandling;
using WebAPI.ExceptionHandling.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IExceptionHandler<Exception>, GenericExceptionHandler>();
builder.Services.AddScoped<IExceptionHandler<ValidationException>, ValidationExceptionHandler>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:44498","http://localhost:44498");
            policy.WithMethods("GET", "POST");
            policy.AllowAnyHeader();
        });
});



var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
    await seeder.SeedData();
}

app.UseHttpsRedirection();

app.UseExceptionHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Make the Program class public so test projects can access it
public partial class Program
{
}