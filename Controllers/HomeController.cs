using Esercizio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Esercizio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Index", "Prenotazione");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Utente utente, bool keepLogged)
        {
            string connString = ConfigurationManager.ConnectionStrings["Hotel"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"SELECT * FROM Utenti WHERE Username = @username AND Password = @password", conn);
            command.Parameters.AddWithValue("@username", utente.Username);
            command.Parameters.AddWithValue("@password", utente.Password);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["IdUtente"].ToString(), keepLogged);
                return RedirectToAction("Index", "Prenotazione");
            }
            TempData["ErrorLogin"] = true;
            return RedirectToAction("Index");
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}