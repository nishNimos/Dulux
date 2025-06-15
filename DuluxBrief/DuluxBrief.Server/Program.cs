using DuluxBrief.Server.Middleware;
using DuluxBrief.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:58667", "https://localhost:5176").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddDbContextFactory<DuluxBrief.Server.Entities.StudentDbContext>(options => options.UseInMemoryDatabase("StudentDb"));
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();
app.UseCors();

// app.UseDefaultFiles();
// app.MapStaticAssets();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthorization();
app.UseExceptionHandling();
app.UseCustomLogging();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
