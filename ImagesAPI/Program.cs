using ImagesAPI.Data;
using ImagesAPI.Models;
using ImagesAPI.Repository;
using ImagesAPI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres") ??
    throw new InvalidOperationException("Connetction string 'postgres' not found.")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IImagesRepostory, ImagesRepository>();
builder.Services.AddScoped<IImagesService, ImagesService>();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
