using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(ConnectionStrings, ServerVersion.AutoDetect(ConnectionStrings))
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Recruteur/Login"; // Spécifiez le chemin d'accès pour la vue de connexion
        options.ExpireTimeSpan = TimeSpan.FromSeconds(3600); // Définir la durée de vie du cookie à 300 secondes (5 minutes)
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 401; // Définir le code d'état HTTP 401 (Non autorisé)
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Recruteur}/{action=Login}/{id?}");

app.Run();