using BackendAPI.DAL;
using BackendAPI.Logic;
using BackendAPI.Model;
using BackendAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = "mongodb://host.docker.internal:27017/";
string dbName = "DockerAzureGitProj_Users";

builder.Services.AddSingleton<MyDBContext>(provider =>
{
    var options = new DbContextOptionsBuilder<MyDBContext>().UseMongoDB(connectionString, dbName).Options;
    var dbContext = new MyDBContext(options);
    return dbContext;
});

var Urls = builder.Configuration.GetSection("FrontendUrls").Get<string[]>()!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(Urls);
    });
});


builder.Services.AddScoped<LogicService>();
builder.Services.AddScoped<RepositoryService>();
builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
