using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Uredi(int StavkaID)
        {
            StavkaUrediVM m = new StavkaUrediVM();


            var s = _database.FakturaStavka.Find(StavkaID);

            m.Kolicina = s.Kolicina;
            m.ProizvodID = s.ProizvodId;

            m.ProizvodStavke = _database
                .Proizvod
                .Select(s=>new SelectListItem
            {
                Text = s.Naziv + " - " + s.Cijena,
                Value = s.Id.ToString()
            }).ToList();

            return View(m);
        }

        public IActionResult Dodaj(int FakturaID)
        {
            var m = new StavkaUrediVM
            {
                ProizvodStavke = 
                    _database
                    .Proizvod
                    .Select(s => new SelectListItem
                    {
                        Text = s.Naziv + " - " + s.Cijena,
                        Value = s.Id.ToString()
                    }).ToList(),

                FakturaID = FakturaID
            };
            return View("Uredi", m);
        }

        public string Obrisi(int StavkaID)
        {
            FakturaStavka x = _database.FakturaStavka.Find(StavkaID);
            _database.Remove(x);
            _database.SaveChanges();

            return "OK";
        }

        public string Snimi(StavkaUrediVM x)
        {
            FakturaStavka stavka;
            if (x.StavkaID == 0)
            {
                //dodavanje
                stavka = new FakturaStavka();
                stavka.FakturaId = x.FakturaID;
            }
            else
            {
                //edit
                stavka = _database.FakturaStavka.Find(x.StavkaID);
            }
            
            stavka.Kolicina = x.Kolicina;
            stavka.ProizvodId = x.ProizvodID;
            _database.SaveChanges();

            return "OK";
        }


    }
}