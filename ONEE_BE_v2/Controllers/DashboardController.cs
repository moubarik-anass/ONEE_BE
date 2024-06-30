using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONEE_BE_v2.Context;
using ONEE_BE_v2.Models;
using System.Net.NetworkInformation;

namespace ONEE_BE_v2.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]

        [HttpGet]
        public int GetActiveOffersCount()
        {
            return _context.Offres.Count(o => o.Status == "active");
        }
        [Authorize]

        [HttpGet]
        public int GetValidatedApplicationsCount()
        {
            return _context.Candidatures.Count(a => a.Status == "valide");
        }
        [Authorize]

        [HttpGet]
        public int GetNonValidatedApplicationsCount()
        {
            return _context.Candidatures.Count(a => a.Status == "non valide");
        }
        [Authorize]
        [HttpGet]
        public int GetPendingApplicationsCount()
        {
            return _context.Candidatures.Count(a => a.Status == "en cours");
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetValidatedApplicationsPerMonth()
        {
            int currentYear = DateTime.Now.Year;

            var validatedApplications = _context.Candidatures
                .Where(c => c.Status == "valide" && c.datepostulation.Year == currentYear)
                .GroupBy(c => c.datepostulation.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.Month)
                .ToList();

            // Assurez-vous d'avoir une entrée pour chaque mois, même si le compte est 0
            var result = Enumerable.Range(1, 12)
                .GroupJoin(validatedApplications,
                    month => month,
                    app => app.Month,
                    (month, apps) => new
                    {
                        Month = month,
                        Count = apps.FirstOrDefault()?.Count ?? 0
                    })
                .OrderBy(x => x.Month)
                .ToList();

            return Json(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetNonValidatedApplicationsPerMonth()
        {
            int currentYear = DateTime.Now.Year;

            var nonValidatedApplications = _context.Candidatures
                .Where(c => c.Status == "non valide" && c.datepostulation.Year == currentYear)
                .GroupBy(c => c.datepostulation.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.Month)
                .ToList();

            var result = Enumerable.Range(1, 12)
                .GroupJoin(nonValidatedApplications,
                    month => month,
                    app => app.Month,
                    (month, apps) => new
                    {
                        Month = month,
                        Count = apps.FirstOrDefault()?.Count ?? 0
                    })
                .OrderBy(x => x.Month)
                .ToList();

            return Json(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPendingApplicationsPerMonth()
        {
            int currentYear = DateTime.Now.Year;

            var pendingApplications = _context.Candidatures
                .Where(c => c.Status == "en cours" && c.datepostulation.Year == currentYear)
                .GroupBy(c => c.datepostulation.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.Month)
                .ToList();

            var result = Enumerable.Range(1, 12)
                .GroupJoin(pendingApplications,
                    month => month,
                    app => app.Month,
                    (month, apps) => new
                    {
                        Month = month,
                        Count = apps.FirstOrDefault()?.Count ?? 0
                    })
                .OrderBy(x => x.Month)
                .ToList();

            return Json(result);
        }


    }

}
