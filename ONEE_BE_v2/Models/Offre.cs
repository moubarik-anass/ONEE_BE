﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ONEE_BE_v2.Models
{
	public class Offre
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Titre est requis.")]
        public string? Titre { get; set; }

        [Required(ErrorMessage = "Date de début est requise.")]
        [DataType(DataType.Date)]
        public DateTime dateDebut { get; set; }

        [Required(ErrorMessage = "Date de fin est requise.")]
        [DataType(DataType.Date)]
        public DateTime dateFin { get; set; }

        [Required(ErrorMessage = "Nombre de places est requis.")]
        [Range(1, int.MaxValue, ErrorMessage = "Le nombre de places doit être supérieur à 0.")]
        public int nbr_places { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
