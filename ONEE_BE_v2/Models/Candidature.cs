using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ONEE_BE_v2.Models
{
    public class Candidature
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? motdepasse { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime datepostulation { get; set; }
        public string? ville { get; set; }
        public string? Adresse { get; set; }
        public string? Emploiprecedent { get; set; }
        public string? nompere { get; set; }
        public string? nommere { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime datenaissance { get; set; }
        public string? statusfamiliale { get; set; }
        public string? CINE { get; set; } 
        public int? TELEphone { get; set; } 
        public string? Spécialite { get; set; }
        public string? description { get; set; }
        public required string Status { get; set; }

        // Navigation property for the related Documents
        public List<Document> Documents { get; set; } = new List<Document>();
        public int OffreId { get; set; }
        // Navigation property for the related Offre
        public Offre Offre { get; set; }
    }

}
