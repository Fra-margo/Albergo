using Esercizio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esercizio.Controllers
{
    [Authorize]
    public class ServizioPrenotazioneController : Controller
    {
        // GET: ServizioPrenotazione
        public ActionResult Add()
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            List<Servizio> servizi = new List<Servizio>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var query = "SELECT * FROM Prenotazioni";
                var command = new SqlCommand(query, conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var prenotazione = new Prenotazione
                    {
                        IdPrenotazione = (int)reader["IdPrenotazione"],
                        DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                        Anno = (int)reader["Anno"],
                        DataArrivo = (DateTime)reader["DataArrivo"],
                        DataPartenza = (DateTime)reader["DataPartenza"],
                        CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                        TariffaApplicata = (decimal)reader["TariffaApplicata"],
                        TipoSoggiorno = (string)reader["TipoSoggiorno"],
                        IdCliente = (int)reader["IdCliente"],
                        IdCamera = (int)reader["IdCamera"],
                        TotaleDaPagare = (decimal)reader["TotaleDaPagare"]
                    };
                    prenotazioni.Add(prenotazione);
                }

                reader.Close();

                query = "SELECT * FROM Servizi";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var servizio = new Servizio
                    {
                        IdServizio = (int)reader["IdServizio"],
                        Descrizione = (string)reader["Descrizione"],
                        Prezzo = (decimal)reader["Prezzo"]
                    };
                    servizi.Add(servizio);
                }

                reader.Close();

                ViewBag.Prenotazione = prenotazioni;
                ViewBag.Servizio = servizi;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ServizioPrenotazione servizio)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    var command = new SqlCommand(@"INSERT INTO [Servizi_Prenotazioni] (IdServizio, IdPrenotazione, DataServizio, Quantità) 
                                           VALUES (@IdServizio, @IdPrenotazione, @DataServizio, @Quantità)", conn);
                    command.Parameters.AddWithValue("@IdServizio", servizio.IdServizio);
                    command.Parameters.AddWithValue("@IdPrenotazione", servizio.IdPrenotazione);
                    command.Parameters.AddWithValue("@DataServizio", servizio.DataServizio);
                    command.Parameters.AddWithValue("@Quantità", servizio.Quantità);

                    var nRows = command.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Prenotazione");
            }
            else
            {
                return View(servizio);
            }
        }
    }
}