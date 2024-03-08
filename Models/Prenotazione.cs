using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class Prenotazione
    {
        public int IdPrenotazione { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public DateTime DataPrenotazione { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Anno { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public DateTime DataArrivo { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public DateTime DataPartenza { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public decimal CaparraConfirmatoria { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public decimal TariffaApplicata { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public decimal TotaleDaPagare { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string TipoSoggiorno { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int IdCamera { get; set; }
        public string CodiceFiscale { get; set; }
        public int NumeroCamera { get; set; }
    }
}