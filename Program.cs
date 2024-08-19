using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>
    (options => options .UseSqlServer(
        "Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

// Configurar autenticação baseada em cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configurar o pipeline de requisição
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization();  // Habilita autorização

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=TelaLogin}");

app.Run();

