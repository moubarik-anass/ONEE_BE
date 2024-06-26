using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;
using Microsoft.AspNetCore.Authorization;

namespace ONEE_BE_v2.Controllers
{
    [Authorize]
    public class CandidaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var offres = _context.Offres
                    .Where(o => o.Status == "active")
                    .Select(o => new { o.Id, o.Titre })
                    .ToList();

            ViewBag.Offres = offres;
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetCandidatures()
        {
            var candidatures = await _context.Candidatures.ToListAsync();
            return Json(candidatures);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidature = await _context.Candidatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidature == null)
            {
                return NotFound();
            }

            return View(candidature);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,motdepasse,datepostulation,ville,Adresse,Emploiprecedent,nompere,nommere,datenaissance,statusfamiliale,Status")] Candidature candidature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidature);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null)
            {
                return NotFound();
            }
            return View(candidature);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,description")] Candidature candidature)
        {
            if (id != candidature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Charger l'entité existante à partir de la base de données
                    var existingCandidature = await _context.Candidatures.FindAsync(id);

                    if (existingCandidature == null)
                    {
                        return NotFound();
                    }

                    // Mettre à jour uniquement la propriété "Status"
                    existingCandidature.Status = candidature.Status;
                    existingCandidature.description = candidature.description;
                    // Mettre à jour l'entité dans le contexte
                    _context.Update(existingCandidature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatureExists(candidature.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidature);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidature = await _context.Candidatures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidature == null)
            {
                return NotFound();
            }

            return View(candidature);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature != null)
            {
                _context.Candidatures.Remove(candidature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatureExists(int id)
        {
            return _context.Candidatures.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> DocumentCin(int candidatureId)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.CandidatureId == candidatureId && d.FileType == "cin");

            if (document == null)
            {
                return NotFound();
            }

            // Exemple pour un PDF
            return File(System.IO.File.OpenRead(document.Path), "application/pdf", document.FileName);
        }

        [Authorize]
        public async Task<IActionResult> DocumentCv(int candidatureId)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.CandidatureId == candidatureId && d.FileType == "cv");

            if (document == null)
            {
                return NotFound();
            }

            // Exemple pour un document texte
            return File(System.IO.File.OpenRead(document.Path), "text/plain", document.FileName);
        }

        [Authorize]
        public async Task<IActionResult> DocumentDiplome(int candidatureId)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.CandidatureId == candidatureId && d.FileType == "diplome");

            if (document == null)
            {
                return NotFound();
            }

            // Exemple pour une image
            return File(System.IO.File.OpenRead(document.Path), "image/jpeg", document.FileName);
        }


        //[Authorize]
        //[HttpGet]
        //public async Task<JsonResult> GetOffresTitles()
        //{
        //    var titres = await _context.Offres
        //        .Where(o => o.Status == "active")
        //        .Select(o => new { o.Id, o.Titre })
        //        .ToListAsync();

        //    return Json(titres);
        //}

    }
}
