using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Application.Contract;
using ToDoListAPI.Application.Implementation;
using ToDoListAPI.Infrastructure;
using ToDoListAPI.Infrastructure.Contract;
using ToDoListAPI.Infrastructure.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<ToDoListCamiloGomezContext>(options =>
    options.UseSqlServer(connectionString));

//Service DI
builder.Services.AddScoped<ITaskService, TaskService>();

//Infrastructure DI
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
