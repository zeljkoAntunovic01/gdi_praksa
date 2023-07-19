using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core.Entities;
using Praksa.Models.SignalR;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using Praksa.Models;
using Praksa.Configuration;
using Praksa.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<MoviesDbContext>
    (configure => configure.UseSqlServer(builder.Configuration
    .GetConnectionString("DefaultConnection"), options =>
    {
        options.MigrationsAssembly(typeof(MoviesDbContext).Assembly.FullName);
    }));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();
app.MapHub<AppHub>("/appHub");

app.Run();
