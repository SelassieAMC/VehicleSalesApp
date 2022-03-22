using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleSales.Infrastructure;
using VehicleSales.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
var corsPolicyName = "OriginsAllowed";

services.AddControllersWithViews();
services.AddSwaggerGen();
services.AddApplicationServices();
services.AddCors(opts =>
{
    opts.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins(allowedOrigins.Split(";"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(corsPolicyName);

using (var serviceScope = app.Services?.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context?.Database.Migrate();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();