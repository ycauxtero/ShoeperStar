using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ShoeperStar;
using ShoeperStar.Data.Contracts;
using ShoeperStar.Data.Repository;
using ShoeperStar.Extensions;
using ShoeperStar.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureSql(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.ConfigureHashIdService(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Authentication and authorization
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.ConfigureEmailService(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);

app.Run();
