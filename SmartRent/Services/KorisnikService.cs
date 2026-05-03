using SmartRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRent.Services
{
    public class KorisnikService
    {
        DBContext _DBContext = new DBContext();

        public bool Register(string username, string lozinka, string ime, string prezime)
        {
            bool exists = _DBContext.Korisnici.Count(k => k.KorisnickoIme == username) > 0;
            if (exists) return false;
            Korisnik korisnik = new Korisnik()
            {
                KorisnickoIme = username,
                Lozinka = lozinka,
                Ime = ime,
                Prezime = prezime
            };

            _DBContext.Korisnici.Add(korisnik);
            _DBContext.SaveChanges();
            return true;
        }

        public bool Login(string username, string lozinka)
        {
            bool exists = _DBContext.Korisnici.Count(k => k.KorisnickoIme == username) > 0;
            if (!exists) return false;
            Korisnik korisnik = _DBContext.Korisnici.Single(k => k.KorisnickoIme == username);
            if (korisnik.Lozinka == lozinka) return true;
            return false;
        }

        public int GetId(string username)
        {
            bool exists = _DBContext.Korisnici.Count(k => k.KorisnickoIme == username) > 0;
            if (!exists) return -1;
            Korisnik korisnik = _DBContext.Korisnici.Single(k => k.KorisnickoIme == username);
            return korisnik.Id;
        }
    }
}