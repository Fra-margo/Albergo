using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Sono previsti SOLO 16 caratteri per il Codice Fiscale")]
        public string CodiceFiscale { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Città { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Provincia { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cellulare { get; set; }
    }
}