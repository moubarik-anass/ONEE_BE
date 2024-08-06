using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;
using Microsoft.AspNetCore.Authorization;

namespace ONEE_BE_v2.Controllers
{
    [Authorize]
    public class Candidatures_eController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Candidatures_eController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var offres = await _context.Offres
                                 .Where(o => o.Status == "active")
                                 .Select(o => new { o.Id, o.Titre })
                                 .ToListAsync();

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
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Motdepasse,DatePostulation,Ville,Adresse,EmploiPrecedent,NomPere,NomMere,DateNaissance,StatusFamiliale,Status")] Candidature candidature)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Description")] Candidature candidature)
        {
            if (id != candidature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCandidature = await _context.Candidatures.FindAsync(id);
                    if (existingCandidature == null)
                    {
                        return NotFound();
                    }

                    existingCandidature.Status = candidature.Status;
                    existingCandidature.description = candidature.description;

                    _context.Update(existingCandidature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CandidatureExistsAsync(candidature.Id))
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
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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

            return File(await System.IO.File.ReadAllBytesAsync(document.Path), "application/pdf", document.FileName);
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

            return File(await System.IO.File.ReadAllBytesAsync(document.Path), "text/plain", document.FileName);
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

            return File(await System.IO.File.ReadAllBytesAsync(document.Path), "image/jpeg", document.FileName);
        }

        private async Task<bool> CandidatureExistsAsync(int id)
        {
            return await _context.Candidatures.AnyAsync(e => e.Id == id);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status, string description)
        {
            try
            {
                var candidature = _context.Candidatures.Find(id);
                if (candidature == null)
                {
                    return NotFound();
                }

                candidature.Status = status;
                if (status == "non validé")
                {
                    candidature.description = description;
                }
                else
                {
                    candidature.description = null;
                }

                _context.Update(candidature);
                _context.SaveChanges();

                return Json(new { success = true, message = "Statut mis à jour avec succès" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur lors de la mise à jour du statut: " + ex.Message });
            }
        }
    }
}