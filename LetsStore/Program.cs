using LetsStore.Mapper;
using LetsStore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Auth/Login";
        option.LogoutPath = "/Auth/Logout";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    });

builder.Services.AddDbContext<LetsStoreContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConn")
));
//Scaffold-DbContext 'Data Source=KAAN\SQLEXPRESS;Initial Catalog=LetsStore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True' Microsoft.EntityFrameworkCore.SqlServer -dir Models
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
