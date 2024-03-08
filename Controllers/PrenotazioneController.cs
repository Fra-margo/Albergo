using Esercizio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Esercizio.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCliente()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {            
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare) VALUES (@CodiceFiscale, @Cognome, @Nome, @Città, @Provincia, @Email, @Telefono, @Cellulare)", conn);
            command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
            command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@Città", cliente.Città);
            command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Telefono", (object)cliente.Telefono ?? DBNull.Value);
            command.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);
            var nRows = command.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("AddPrenotazione", "Prenotazione");
            }
            else
            {
                return View(cliente);
            }
        }
        public ActionResult AddPrenotazione()
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            List<Cliente> clienti;
            List<Camera> camere;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var query = "SELECT IdCliente, CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare FROM Clienti";
                var command = new SqlCommand(query, conn);
                var reader = command.ExecuteReader();

                clienti = new List<Cliente>();

                while (reader.Read())
                {
                    var cliente = new Cliente
                    {
                        IdCliente = (int)reader["IdCliente"],
                        CodiceFiscale = reader["CodiceFiscale"] != DBNull.Value ? (string)reader["CodiceFiscale"] : null,
                        Cognome = reader["Cognome"] != DBNull.Value ? (string)reader["Cognome"] : null,
                        Nome = reader["Nome"] != DBNull.Value ? (string)reader["Nome"] : null,
                        Città = reader["Città"] != DBNull.Value ? (string)reader["Città"] : null,
                        Provincia = reader["Provincia"] != DBNull.Value ? (string)reader["Provincia"] : null,
                        Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                        Telefono = reader["Telefono"] != DBNull.Value ? (string)reader["Telefono"] : null,
                        Cellulare = reader["Cellulare"] != DBNull.Value ? (string)reader["Cellulare"] : null
                    };
                    clienti.Add(cliente);
                }
            }

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var query = "SELECT IdCamera, NumeroCamera, Descrizione, Prezzo FROM Camere";
                var command = new SqlCommand(query, conn);
                var reader = command.ExecuteReader();
                camere = new List<Camera>();

                while (reader.Read())
                {
                    var camera = new Camera
                    {
                        IdCamera = (int)reader["IdCamera"],
                        NumeroCamera = (int)reader["NumeroCamera"],
                        Descrizione = reader["Descrizione"] != DBNull.Value ? (string)reader["Descrizione"] : null,
                        Prezzo = reader["Prezzo"] != DBNull.Value ? (decimal)reader["Prezzo"] : 0
                    };
                    camere.Add(camera);
                }
            }

            ViewBag.Cliente = clienti;
            ViewBag.Camera = camere;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid) { 
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"INSERT INTO Prenotazioni (DataPrenotazione, Anno, DataArrivo, DataPartenza, CaparraConfirmatoria, TariffaApplicata, TotaleDaPagare, TipoSoggiorno, IdCliente, IdCamera) VALUES (@DataPrenotazione, @Anno, @DataArrivo, @DataPartenza, @CaparraConfirmatoria, @TariffaApplicata, @TotaleDaPagare, @TipoSoggiorno, @IdCliente, @IdCamera)", conn);
            command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
            command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
            command.Parameters.AddWithValue("@DataArrivo", prenotazione.DataArrivo);
            command.Parameters.AddWithValue("@DataPartenza", prenotazione.DataPartenza);
            command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
            command.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
            command.Parameters.AddWithValue("@TotaleDaPagare", prenotazione.TotaleDaPagare);
            command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);
            command.Parameters.AddWithValue("@IdCliente", prenotazione.IdCliente);
            command.Parameters.AddWithValue("@IdCamera", prenotazione.IdCamera);
            var nRows = command.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
            }
            else
            {
                return View(prenotazione);
            }
        }

        public ActionResult AllPrenotazioni()
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"SELECT Prenotazioni.IdPrenotazione,
                                        Prenotazioni.DataPrenotazione,
                                        Prenotazioni.Anno,
                                        Prenotazioni.DataArrivo,
                                        Prenotazioni.DataPartenza,
                                        Prenotazioni.CaparraConfirmatoria,
                                        Prenotazioni.TariffaApplicata,
                                        Prenotazioni.TipoSoggiorno,
                                        Clienti.CodiceFiscale,
                                        Camere.NumeroCamera,
                                        Prenotazioni.TotaleDaPagare
                                    FROM Prenotazioni
                                    INNER JOIN Clienti ON Prenotazioni.IdCliente = Clienti.IdCliente
                                    INNER JOIN Camere ON Prenotazioni.IdCamera = Camere.IdCamera", conn);
            var reader = command.ExecuteReader();
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prenotazione = new Prenotazione()
                    {
                        IdPrenotazione = (int)reader["IdPrenotazione"],
                        DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                        Anno = (int)reader["Anno"],
                        DataArrivo = (DateTime)reader["DataArrivo"],
                        DataPartenza = (DateTime)reader["DataPartenza"],
                        CaparraConfirmatoria = (decimal)reader["CaparraConfirmatoria"],
                        TariffaApplicata = (decimal)reader["TariffaApplicata"],
                        TipoSoggiorno = (string)reader["TipoSoggiorno"],
                        CodiceFiscale = (string)reader["CodiceFiscale"],
                        NumeroCamera = (int)reader["NumeroCamera"],
                        TotaleDaPagare = (decimal)reader["TotaleDaPagare"]
                    };
                    prenotazioni.Add(prenotazione);
                }
            }
            return View(prenotazioni);
        }
    }
}