using SmartRent.Models;
using SmartRent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRent.Controllers
{
    public class HomeController : Controller
    {
        bool Ulogovan()
        {
            object obj = Session["korisnik"];
            if (obj == null) return false;
            string s = obj as string;
            if (s == null) return false;
            return true;
        }

        AutomobilService automobilService = new AutomobilService();
        IznajmljivanjeService iznajmljivanjeService = new IznajmljivanjeService();
        KorisnikService korisnikService = new KorisnikService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vozila()
        {
            if (!Ulogovan()) return RedirectToAction("Login", "Account");
            return View(automobilService.GetAutomobili());
        }

        [HttpGet]
        public ActionResult Vozila(string s)
        {
            if (!Ulogovan()) return RedirectToAction("Login", "Account");


            List<Automobil> automobili = automobilService.GetAutomobili().ToList();
            if (s == null || s == "") return View(automobili);

            automobili = automobili.Where(a =>
            {
                bool yes = false;

                string autoStr = a.ToLongString();
                string[] sTerms = s.Split(' ');

                foreach (var term in sTerms)
                {
                    yes = autoStr.ToLower().Contains(term.ToLower());
                }

                return yes;
            }).ToList();

            return View(automobili);
        }

        public ActionResult Details(int id)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");
            return View(automobilService.GetAutomobil(id)); 
        }

        [HttpPost]
        public ActionResult Details(int id, string pocetak, string kraj)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());

            if (korisnikId == -1) return RedirectToAction("Login", "Account");

            DateTime pocetakDate = DateTime.Parse(pocetak);
            DateTime krajDate = DateTime.Parse(kraj);

            if (pocetakDate > krajDate)
            {
                ViewBag.Message = "Datum vracanja ne sme biti pre datuma iznajmljivanja";
                return View(automobilService.GetAutomobil(id));
            }

            string msg = iznajmljivanjeService.Iznajmi(korisnikId,id,pocetakDate,krajDate);

            ViewBag.Message = msg;
            return View(automobilService.GetAutomobil(id));
        }

        public ActionResult Rezervacije()
        {
            if (!Ulogovan()) return RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());

            return View(iznajmljivanjeService.GetIznajmljivanja(korisnikId));
        }

        public ActionResult ObrisiRezervaciju(int id)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());

            Iznajmljivanje i = iznajmljivanjeService.GetIznajmljivanje(id,korisnikId);

            if (i.Id < 0) return RedirectToAction("Rezervacije");

            return View(i);
        }

        [HttpPost, ActionName("ObrisiRezervaciju")]
        public ActionResult ZaistaObrisiRezervaciju(int id)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());
            
            Iznajmljivanje i = iznajmljivanjeService.ObrisiIznajmljivanje(id,korisnikId);

            if (i.Id < 0) return RedirectToAction("Rezervacije");

            return RedirectToAction("Rezervacije");
        }

        public ActionResult UrediRezervaciju(int id)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());

            Iznajmljivanje i = iznajmljivanjeService.GetIznajmljivanje(id, korisnikId);

            if (i.Id < 0) return RedirectToAction("Rezervacije");

            return View(i);
        }

        [HttpPost]
        public ActionResult UrediRezervaciju(int id, string pocetak, string kraj)
        {
            if (!Ulogovan()) RedirectToAction("Login", "Account");

            int korisnikId = korisnikService.GetId(Session["korisnik"].ToString());

            Iznajmljivanje i = iznajmljivanjeService.GetIznajmljivanje(id, korisnikId);

            if (i.Id < 0) return RedirectToAction("Rezervacije");

            DateTime pocetakDate = DateTime.Parse(pocetak);
            DateTime krajDate = DateTime.Parse(kraj);

            if (pocetakDate > krajDate)
            {
                ViewBag.Message = "Datum vracanja ne sme biti pre datuma iznajmljivanja";
                return View(i);
            }

            string msg = iznajmljivanjeService.UrediIznajmljivanje(id, pocetakDate, krajDate, korisnikId);

            if (msg.Length > 0)
            {
                ViewBag.Message = msg;
                return View(i);
            }

            return RedirectToAction("Rezervacije");
        }
    }
}