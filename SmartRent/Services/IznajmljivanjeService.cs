using SmartRent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SmartRent.Services
{
    public class IznajmljivanjeService
    {
        DBContext _DBContext = new DBContext();

        public string Iznajmi(int korisnikId, int automobilId, DateTime pocetak, DateTime kraj)
        {
            Korisnik korisnik = _DBContext.Korisnici.Find(korisnikId);
            Automobil automobil = _DBContext.Automobili.Find(automobilId);

            List<Iznajmljivanje> iznajmljivanja = _DBContext.Iznajmljivanja.Include(i=>i.Automobil).Where(i=>i.Automobil.Id == automobilId).ToList();
            if (iznajmljivanja.Count > 0)
            {
                foreach (var item in iznajmljivanja)
                {
                    DateTime pocetakDb = item.DatumIznajmljivanja;
                    DateTime krajDb = item.DatumVracanja;
                    string errMsg = $"Automobil je vec izdat od {pocetakDb.ToShortDateString()} do {krajDb.ToShortDateString()}";
                    if (pocetak <= krajDb && pocetak >= pocetakDb) return errMsg;
                    if (kraj >= pocetakDb && kraj <= krajDb) return errMsg;
                }
            }

            Iznajmljivanje iznajmljivanje = new Iznajmljivanje()
            {
                Korisnik = korisnik,
                Automobil = automobil,
                DatumIznajmljivanja = pocetak,
                DatumVracanja = kraj
            };

            _DBContext.Iznajmljivanja.AddOrUpdate(iznajmljivanje);
            _DBContext.SaveChanges();
            return $"Automobil je uspesno rezerisan od {pocetak.ToShortDateString()} do {kraj.ToShortDateString()}";
        }
        public IEnumerable<Iznajmljivanje> GetIznajmljivanja(int korisnikId)
        {
            return _DBContext.Iznajmljivanja.Include(i => i.Korisnik).Include(i => i.Automobil.Model.Marka).Where(i => i.Korisnik.Id == korisnikId);
        }

        public Iznajmljivanje GetIznajmljivanje(int id,int korisnikId)
        {
            List<Iznajmljivanje> lista = _DBContext.Iznajmljivanja.Include(i => i.Korisnik).Include(i => i.Automobil.Model.Marka).Where(i => i.Id == id).ToList();
            if (lista.Count == 0) return new Iznajmljivanje() { Id = -1 };
            if (lista[0].Korisnik.Id != korisnikId) return new Iznajmljivanje() { Id = -2 };
            return lista[0];
        }
        public Iznajmljivanje ObrisiIznajmljivanje(int id, int korisnikId)
        {
            Iznajmljivanje iz = new Iznajmljivanje() { Id = -1};
            List<Iznajmljivanje> lista = _DBContext.Iznajmljivanja.Include(i => i.Korisnik).Include(i => i.Automobil.Model.Marka).Where(i => i.Id == id).ToList();
            if (lista.Count == 0) iz = new Iznajmljivanje() { Id = -1 };
            iz = lista[0];
            if (lista[0].Korisnik.Id != korisnikId) iz = new Iznajmljivanje() { Id = -2 };

            if (iz.Id < 0) return iz;

            _DBContext.Iznajmljivanja.Remove(iz);
            _DBContext.SaveChanges();

            return iz;
        }

        public string UrediIznajmljivanje(int id, DateTime pocetak, DateTime kraj, int korisnikId)
        {
            Korisnik korisnik = _DBContext.Korisnici.Find(korisnikId);
            Iznajmljivanje iznajmljivanje = _DBContext.Iznajmljivanja.Include(i => i.Automobil).Include(i => i.Korisnik).SingleOrDefault(i => i.Id == id);

            if (iznajmljivanje == null) return "";

            List<Iznajmljivanje> iznajmljivanja = _DBContext.Iznajmljivanja.Include(i => i.Automobil).Where(i => i.Automobil.Id == iznajmljivanje.Automobil.Id).ToList();
            if (iznajmljivanja.Count > 0)
            {
                foreach (var item in iznajmljivanja)
                {
                    if (item.Id == iznajmljivanje.Id) continue;
                    DateTime pocetakDb = item.DatumIznajmljivanja;
                    DateTime krajDb = item.DatumVracanja;
                    string errMsg = $"Automobil je vec izdat od {pocetakDb.ToShortDateString()} do {krajDb.ToShortDateString()}";
                    if (pocetak <= krajDb && pocetak >= pocetakDb) return errMsg;
                    if (kraj >= pocetakDb && kraj <= krajDb) return errMsg;
                }
            }

            iznajmljivanje.DatumIznajmljivanja = pocetak;
            iznajmljivanje.DatumVracanja = kraj;

            _DBContext.SaveChanges();

            return "";
        }
    }

}