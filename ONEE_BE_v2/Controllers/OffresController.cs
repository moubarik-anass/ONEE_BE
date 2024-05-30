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
        public JsonResult Insert(Offre model)
        {
            if (ModelState.IsValid)
            {
                _context.Offres.Add(model);
                _context.SaveChanges();
                return Json("Les détails de l'offre sont enregistrés");
            }
            else
            {
                return Json("Problème de validation");
            }
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            var offre = _context.Offres.Find(id);
            return Json(offre);
        }

        [HttpPost]
        public JsonResult Update(Offre model)
        {
            if (ModelState.IsValid)
            {
                _context.Offres.Update(model);
                _context.SaveChanges();
                return Json("Les détails de l'offre sont modifiés");
            }
            else
            {
                return Json("Problème de validation   -UPDATE");
            }
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