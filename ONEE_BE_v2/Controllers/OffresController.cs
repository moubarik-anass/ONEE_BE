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

        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOffres()
        {
            var offres = _context.Offres.ToList();
            return Json(offres);
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
                return Json("Problème de validation");
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