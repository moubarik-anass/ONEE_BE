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
        options.LoginPath = "/Recruteur/Login"; // Sp�cifiez le chemin d'acc�s pour la vue de connexion
        options.ExpireTimeSpan = TimeSpan.FromSeconds(3600); // D�finir la dur�e de vie du cookie � 300 secondes (5 minutes)
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 401; // D�finir le code d'�tat HTTP 401 (Non autoris�)
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