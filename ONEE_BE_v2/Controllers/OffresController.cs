using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;
using Microsoft.AspNetCore.Authorization;

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
                // Gérer l'exception ici, par exemple, en journalisant l'erreur
                // Vous pouvez également retourner un message d'erreur approprié ou une réponse JSON avec un code d'erreur
                // Pour l'exemple, je vais simplement retourner une réponse JSON avec le message de l'exception
                return Json(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Insert([FromBody] Offre offres)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    using (var dbContext = _context)
                    {
                        offres.Status = "active";
                        dbContext.Offres.Add(offres);
                        dbContext.SaveChanges();
                        return Json("Les détails de l'offre sont enregistrés");
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message });
                }
            //}
            //else
            //{
            //    // Log the ModelState errors
            //    var errors = ModelState
            //        .Where(ms => ms.Value.Errors.Count > 0)
            //        .Select(ms => new
            //        {
            //            Field = ms.Key,
            //            Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
            //        });

            //    return Json(new { message = "Problème de validation --Insert", errors = errors });
            //}
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
            //if (ModelState.IsValid)
            //{
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
            //}
            //else
            //{
            //    // Log the ModelState errors
            //    var errors = ModelState
            //        .Where(ms => ms.Value.Errors.Count > 0)
            //        .Select(ms => new
            //        {
            //            Field = ms.Key,
            //            Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
            //        });

            //    return Json(new { message = "Problème de validation --Update", errors = errors });
            //}
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

                // Vérifier si la date de fin de l'offre est égale à aujourd'hui
                var today = DateTime.Today;
                if (offre.dateFin == today)
                {
                    // Mettre à jour le statut de l'offre à "archive"
                    offre.Status = "archive";
                }
                else
                {
                    // Sinon, mettre à jour le statut de l'offre à "archivee"
                    offre.Status = "archivee";
                }

                // Enregistrer les modifications dans la base de données
                await _context.SaveChangesAsync();

                string message = offre.Status == "archive"
                    ? "Offre archivée avec succès (date de fin aujourd'hui)"
                    : "Offre archivée avec succès";

                return Json(new { success = true, message });
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici, par exemple, en journalisant l'erreur
                // Vous pouvez également retourner un message d'erreur approprié ou une réponse JSON avec un code d'erreur
                // Pour l'exemple, je vais simplement retourner une réponse JSON avec le message de l'exception
                return Json(new { success = false, message = $"Erreur lors de l'archivage de l'offre : {ex.Message}" });
            }
        }

        //Offres archivées
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
                var offres = await _context.Offres.ToListAsync();
                return Json(offres);
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici, par exemple, en journalisant l'erreur
                // Vous pouvez également retourner un message d'erreur approprié ou une réponse JSON avec un code d'erreur
                // Pour l'exemple, je vais simplement retourner une réponse JSON avec le message de l'exception
                return Json(new { error = ex.Message });
            }
        }
    }
}