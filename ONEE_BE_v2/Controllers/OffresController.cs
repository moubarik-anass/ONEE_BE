using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace ONEE_BE_v2.Controllers
{
    [Authorize]
    public class OffresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index1()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetOffres()
        {
            try
            {
                var offres = await _context.Offres.ToListAsync();
                return Json(offres);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Insert([FromForm] Offre offre, IFormFile Path)
        {
            try
            {
                if (Path != null && Path.Length > 0)
                {
                    var uploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.FileName;
                    var filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Path.CopyTo(fileStream);
                    }
                    offre.Path = "/uploads/" + uniqueFileName; // Chemin relatif pour l'accès web
                }
                offre.Status = "active";
                _context.Offres.Add(offre);
                _context.SaveChanges();
                return Json(new { success = true, message = "Les détails de l'offre sont enregistrés" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return Json("Aucune offre trouvée avec cet ID.");
            }
            return Json(offre);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Update([FromBody] Offre offres)
        {
            var existingOffre = _context.Offres.Find(offres.Id);
            if (existingOffre != null)
            {
                existingOffre.Titre = offres.Titre;
                existingOffre.dateDebut = offres.dateDebut;
                existingOffre.dateFin = offres.dateFin;
                existingOffre.nbr_places = offres.nbr_places;
                existingOffre.Description = offres.Description;

                _context.Offres.Update(existingOffre);
                _context.SaveChanges();
                return Json("Les détails de l'offre sont modifiés");
            }
            else
            {
                return Json("Aucune offre trouvée avec cet ID.");
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre != null)
            {
                _context.Offres.Remove(offre);
                _context.SaveChanges();
                return Json("Les détails de l'offre sont supprimés");
            }
            else
            {
                return Json("Problème de validation");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> Archive(int id)
        {
            try
            {
                var offre = await _context.Offres.FindAsync(id);
                if (offre == null)
                {
                    return Json(new { success = false, message = "Offre non trouvée" });
                }

                var today = DateTime.Today;
                if (offre.dateFin == today)
                {
                    offre.Status = "archivee";
                }
                await _context.SaveChangesAsync();

                string message = offre.Status == "archivee"
                    ? "Offre archivée avec succès (date de fin aujourd'hui)"
                    : "Offre archivée avec succès";

                return Json(new { success = true, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erreur lors de l'archivage de l'offre : {ex.Message}" });
            }
        }

        [Authorize]
        public IActionResult Index0()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetOffres1()
        {
            try
            {
                var offres = await _context.Offres
                    .Where(o => o.Status == "archivee")
                    .ToListAsync();
                return Json(offres);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // Nouvelle action pour télécharger le fichier
        [Authorize]
        [HttpGet]
        public IActionResult DownloadFile(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null || string.IsNullOrEmpty(offre.Path))
            {
                return NotFound("Fichier non trouvé.");
            }

            // Construire le chemin complet du fichier
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", offre.Path.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Fichier non trouvé.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }



    }
}
