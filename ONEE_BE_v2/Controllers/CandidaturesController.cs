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
    public class CandidaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCandidatures()
        {
            var candidatures = await _context.Candidatures.ToListAsync();
            return Json(candidatures);
        }

        // GET: Candidatures/Details/5
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

        // GET: Candidatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Candidatures/Edit/5
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

        // POST: Candidatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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


        // GET: Candidatures/Delete/5
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

        // POST: Candidatures/Delete/5
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
    }
}
