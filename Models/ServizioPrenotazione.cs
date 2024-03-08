using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class ServizioPrenotazione
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int IdServizio { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int IdPrenotazione { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public DateTime DataServizio { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Quantità { get; set; }
        public string Descrizione { get; set; }
    }
}