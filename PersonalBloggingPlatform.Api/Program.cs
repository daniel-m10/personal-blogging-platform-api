using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonalBloggingPlatform.Application.Contracts.Articles;
using PersonalBloggingPlatform.Application.Services;
using PersonalBloggingPlatform.Application.Validation;
using PersonalBloggingPlatform.Domain.Interfaces;
using PersonalBloggingPlatform.Infrastructure.DataAccess;
using PersonalBloggingPlatform.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Personal Blogging Platform API",
        Version = "v1",
        Description = "API for managing personal blogging platform functionalities."
    });
});

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlite("Data Source=App_Data/blog.db");
});
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<IArticleRepository, EfArticleRepository>();
builder.Services.AddScoped<IValidator<CreateArticleRequest>, CreateArticleRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateArticleRequest>, UpdateArticleRequestValidator>();

var app = builder.Build();

// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();