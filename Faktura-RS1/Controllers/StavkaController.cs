using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_Faktura.EF;
using RS1_Faktura.Models;
using RS1_Faktura.ViewModels;

namespace RS1_Faktura.Controllers
{
    public class StavkaController : Controller
    {
        MojContext _database = new MojContext();
        public IActionResult Index(int FakturaID)
        {
            StavkaIndexVM m = new StavkaIndexVM();
            m.rows = _database.FakturaStavka.Where(s => s.FakturaId == FakturaID).Select(s => new StavkaIndexVM.Row
            {
                StavkaID = s.Id,
                Iznos = s.Kolicina* s.Proizvod.Cijena * (1 - s.PopustProcenat/100.0),
                Cijena =s.Proizvod.Cijena,
                Kolicina = s.Kolicina,
                NazivProizvoda = s.Proizvod.Naziv,
                Popust = s.PopustProcenat
            }).ToList();
            m.FakturaID = FakturaID;

            
            return View(m);
        }
    }
}