using Microsoft.EntityFrameworkCore;
using ONEE_BE_v2.Models;

namespace ONEE_BE_v2.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }

    }
}
