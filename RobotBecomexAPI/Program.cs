using RobotBecomexAPI.Models;
using RobotBecomexAPI.Repositories;
using RobotBecomexAPI.Repositories.Interfaces;
using RobotBecomexAPI.Services;
using RobotBecomexAPI.Services.Interfaces;

var MyAllowSpecificOrigins = "robotBecomex";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRobotService, RobotService>();
builder.Services.AddScoped<IRobotRepository, RobotRepository>();
//builder.Services.AddSingleton<Robot>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
