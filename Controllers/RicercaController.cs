using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Esercizio.Models;


namespace Esercizio.Controllers
{
    [Authorize]
    public class RicercaController : Controller
    {
        static string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
        SqlConnection conn = new SqlConnection(connString);
        List<Cliente> clienti = new List<Cliente>();
        // GET: Ricerca
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult RicercaCodiceFiscale(string CodiceFiscale)
        {
            conn.Open();
            var command = new SqlCommand("Select * From Clienti where CodiceFiscale = @CodiceFiscale", conn);
            command.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var cliente = new Cliente()
                {
                    Cognome = (string)reader["Cognome"],
                    Nome = (string)reader["Nome"],
                    Città = (string)reader["Città"],
                    Provincia = (string)reader["Provincia"],
                    Email = (string)reader["Email"],
                    Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                    Cellulare = (string)reader["Provincia"],
                };
                clienti.Add(cliente);
            }
            conn.Close();
            return Json(clienti, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RicercaPensioneCompleta()
        {
            int count = 0;
            conn.Open();
            var command = new SqlCommand($"Select count(*) as TotalePensioneCompleta From Prenotazioni WHERE TipoSoggiorno = 'Pensione Completa'", conn);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                count = (int)reader["TotalePensioneCompleta"];
            }
            conn.Close();
            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }
}