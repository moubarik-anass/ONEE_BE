using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;

namespace ONEE_BE_v2.Controllers
{
    public class RecruteurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecruteurController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string matricule, string motDePasse)
        {
            var recruteur = await _context.Recruteurs.FirstOrDefaultAsync(r => r.Matricule == matricule && r.MotDePasse == motDePasse);

            if (recruteur != null)
            {
                // Créer un cookie d'authentification
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, recruteur.Matricule),
            new Claim(ClaimTypes.Email, recruteur.Email),
            new Claim("Nom", recruteur.Nom),
            new Claim("Prenom", recruteur.Prenom)
            // Ajoutez d'autres revendications si nécessaire
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false, // Ne pas conserver le cookie après la fermeture du navigateur
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15) // Définir la durée de vie du cookie à 300 secondes (5 minutes)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index1", "Offres");
            }
            else
            {
                // Authentification échouée
                ModelState.AddModelError(string.Empty, "Matricule ou mot de passe invalide.");
                return View();
            }
        }

        [HttpPost]
               public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Supprimer le cookie d'authentification
            return RedirectToAction("Login"); // Rediriger vers la page de connexion
        }
    }

}