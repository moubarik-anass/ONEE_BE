using System;
using System.ComponentModel.DataAnnotations;

namespace ONEE_BE_v2.Models
{
    public class Archives : Offre
    {
        // Ajoutez des propriétés spécifiques aux archives ici si nécessaire

        // Constructeur par défaut
        public Archives() : base()
        {
            // Initialisez les propriétés spécifiques aux archives si nécessaire
        }
        [DataType(DataType.Date)]
        public DateTime dateArchivage { get; set; }
    }
}
