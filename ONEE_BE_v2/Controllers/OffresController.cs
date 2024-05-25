using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if(ModelState.IsValid)
            {
                _context.Offres.Add(model);
                _context.SaveChanges();
                return Json("les details d offres sont enregistrées");
            }
            else
            {
                return Json("Problème de validation");
            }
        }
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var offres = _context.Offres.Find(id);
            return Json(offres);
        }
        [HttpPost]
        public JsonResult Update(Offre model)
        {
            if (ModelState.IsValid)
            {
                _context.Offres.Update(model);
                _context.SaveChanges();
                return Json("les details d offres sont modifées");
            }
            else
            {
                return Json("Problème de validation");
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var offres = _context.Offres.Find(id);
            if (offres != null)
            {
                _context.Offres.Remove(offres);
                _context.SaveChanges();
                return Json("les details d offres sont supprimées");
            }
            else
            {
                return Json("Problème de validation");
            }
        }
    }
}
