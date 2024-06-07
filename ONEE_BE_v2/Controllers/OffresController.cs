using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;

namespace ONEE_BE_v2.Controllers
{
    public class OffresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffresController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index1()
        {
            return View();
        }

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



		[HttpPost]
        public JsonResult Insert([FromBody] Offre offres)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = _context)
                    {
                        dbContext.Offres.Add(offres);
                        dbContext.SaveChanges();
                        return Json("Les détails de l'offre sont enregistrés");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and return an error message
                    return Json(new { error = ex.Message });
                }

                //_context.Offres.Add(offres);
                //_context.SaveChanges();
                // return Json("Les détails de l'offre sont enregistrés");
            }

            else
            {
                // Log the ModelState errors
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    });

                return Json(new { message = "Problème de validation --Insert", errors = errors });
            }
        }

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


        [HttpPost]
        public JsonResult Update([FromBody] Offre offres)
        {
            if (ModelState.IsValid)
            {
                _context.Offres.Update(offres);
                _context.SaveChanges();
                return Json("Les détails de l'offre sont modifiés");
            }
            else
            {
                return Json("Problème de validation   -UPDATE");
            }/*if (ModelState.IsValid)
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
            }*/
        }

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
    }
}