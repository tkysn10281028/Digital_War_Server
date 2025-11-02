using System.Reflection;
using ApiServer.Project.Common;
using ApiServer.Project.Database;
using ApiServer.Project.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    if (builder.Environment.IsEnvironment("Docker"))
    {
        c.AddServer(new OpenApiServer { Url = "/api" });
    }
    else if (builder.Environment.IsDevelopment())
    {
        c.AddServer(new OpenApiServer { Url = "/" });
    }
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var assembly = Assembly.GetExecutingAssembly();
var baseType = typeof(IInjectable);

foreach (var type in assembly.GetTypes()
         .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t)))
{
    builder.Services.AddScoped(type);
}

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DatabaseSeeder.Seed(db);
}
app.MapControllers();
app.Run();