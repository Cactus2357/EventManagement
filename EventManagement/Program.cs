using EventManagement.Hubs;
using EventManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR(o => o.EnableDetailedErrors = true);

// Register the DbContext with dependency injection
builder.Services.AddDbContext<EventManagementContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Signin";
        options.LogoutPath = "/Auth/Signout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<EventHub>("/EventHub");
app.MapGet("/", context =>
{
    context.Response.Redirect("/Events/Index");
    return Task.CompletedTask;
});

app.UseStatusCodePagesWithReExecute("/Shared/Error{0}");

app.MapRazorPages();

app.Run();
