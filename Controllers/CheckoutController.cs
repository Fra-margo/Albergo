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
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout(int idPrenotazione)
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            Prenotazione prenotazione = null;
            List<Servizio> serviziAggiuntivi = new List<Servizio>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var query = "SELECT * FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    prenotazione = new Prenotazione
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
                }
                reader.Close();

                
                query = "SELECT s.* FROM Servizi s INNER JOIN Servizi_Prenotazioni sp ON s.IdServizio = sp.IdServizio WHERE sp.IdPrenotazione = @IdPrenotazione";
                command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var servizio = new Servizio
                    {
                        IdServizio = (int)reader["IdServizio"],
                        Descrizione = (string)reader["Descrizione"],
                        Prezzo = (decimal)reader["Prezzo"]
                    };
                    serviziAggiuntivi.Add(servizio);
                }
                reader.Close();
            }

            
            decimal importoTotale = prenotazione.TotaleDaPagare + serviziAggiuntivi.Sum(s => s.Prezzo);

            
            ViewBag.Prenotazione = prenotazione;
            ViewBag.ServiziAggiuntivi = serviziAggiuntivi;
            ViewBag.ImportoTotale = importoTotale;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int IdPrenotazione)
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var deleteServiziQuery = "DELETE FROM Servizi_Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                var deleteServiziCommand = new SqlCommand(deleteServiziQuery, conn);
                deleteServiziCommand.Parameters.AddWithValue("@IdPrenotazione", IdPrenotazione);
                deleteServiziCommand.ExecuteNonQuery();
            }

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var deletePrenotazioneQuery = "DELETE FROM Prenotazioni WHERE IdPrenotazione = @IdPrenotazione";
                var deletePrenotazioneCommand = new SqlCommand(deletePrenotazioneQuery, conn);
                deletePrenotazioneCommand.Parameters.AddWithValue("@IdPrenotazione", IdPrenotazione);
                deletePrenotazioneCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "Prenotazione");
        }
    }
}