using Bazaar.Models.AllDbContexts;
using Bazaar.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? conStr = builder.Configuration.GetConnectionString("mydatabase");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite(conStr));

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(conStr));
    
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();
// Configure the Application Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // If the LoginPath isn't set, ASP.NET Core defaults the path to /Account/Login.
    options.LoginPath = "/Home/Login"; // Set your login path here
    // If the AccessDenied isn't set, ASP.NET Core defaults the path to /Account/AccessDenied
    options.AccessDeniedPath = "/Home/AccessDenied"; // Set your access denied path here
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
