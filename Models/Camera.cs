using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class Camera
    {
        public int IdCamera { get; set; }
        public int NumeroCamera { get; set; }
        public string Descrizione { get; set; }
        public string Tipologia { get; set; }
        public decimal Prezzo { get; set; }
    }
}