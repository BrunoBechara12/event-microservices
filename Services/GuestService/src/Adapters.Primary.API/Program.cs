using Adapters.Primary.API.Extensions;
using Adapters.Secondary;
using Adapters.Secondary.Context;
using Application.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddDbContext<GuestDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataBaseService(builder.Configuration);
builder.Services.AddGuestUseCase();

builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "EventMicroservices v1");

    options.ConfigObject.AdditionalItems["operationsSorter"] = "method";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
