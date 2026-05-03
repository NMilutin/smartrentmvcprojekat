using SmartRent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRent.Controllers
{
    public class AccountController : Controller
    {
        KorisnikService korisnikService = new KorisnikService();
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string lozinka)
        {
            bool tacno = korisnikService.Login(username, lozinka);
            if (tacno)
            {
                Session["korisnik"] = username;
                return RedirectToAction("Vozila", "Home");
            }
            ViewBag.Message = "Korisnik ne postoji ili je lozinka pogresna";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string lozinka, string ponovi,string ime, string prezime="")
        {
            if (username.Length > 20)
            {
                ViewBag.Message = "Korisnicko ne sme biti duze od 20 karaktera";
                return View();
            }
            if (lozinka != ponovi)
            {
                ViewBag.Message = "Lozinke moraju biti iste";
                return View();
            }
            if (lozinka.Length < 8)
            {
                ViewBag.Message = "Lozinka mora biti dugacka barem 8 karaktera";
                return View();
            }
            bool uspeh = korisnikService.Register(username, lozinka, ime, prezime);
            if (!uspeh)
            {
                ViewBag.Message = "Korisnik vec postoji";
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}