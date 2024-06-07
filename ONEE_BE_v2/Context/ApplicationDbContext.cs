using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Models;

namespace ONEE_BE_v2.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Déclaration de DbSet pour Offres
        public DbSet<Offre> Offres { get; set; }

        // Déclaration de DbSet pour Candidatures
        public DbSet<Candidature> Candidatures { get; set; }

        // Déclaration de DbSet pour Archives
        //public DbSet<Archives> Archives { get; set; }
    }
}
