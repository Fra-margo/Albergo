using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercizio.Models
{
    public class Servizio
    {
        public int IdServizio { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public int Quantità { get; set; }
    }
}