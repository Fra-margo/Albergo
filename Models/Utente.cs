using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class Utente
    {
        [Key]
        public int IdUtente { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Password { get; set; }
    }
}