using EventManagement.Hubs;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR(o => o.EnableDetailedErrors = true);

// Register the DbContext with dependency injection
builder.Services.AddDbContext<EventManagementContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<EventHub>("/EventHub");
app.MapGet("/", context =>
{
    context.Response.Redirect("/Events");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
