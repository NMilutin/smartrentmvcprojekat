using SmartRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SmartRent.Services
{
    public class AutomobilService
    {
        DBContext _DBContext = new DBContext();
        public IEnumerable<Automobil> GetAutomobili()
        {
            return _DBContext.Automobili.Include(a=>a.Model).Include(a=>a.Model.Marka).ToArray();
        }

        public Automobil GetAutomobil(int id)
        {
            return _DBContext.Automobili.Include(a=>a.Model).Include(a=>a.Model.Marka).Single(a=>a.Id== id);
        }
    }
}