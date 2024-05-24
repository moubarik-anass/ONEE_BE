using System.ComponentModel.DataAnnotations;

namespace ONEE_BE_v2.Models
{
    public class Offre
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Titre { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateDebut { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateFin { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Le nombre de places doit être supérieur ou égal à 1")]
        public int nbr_places { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
