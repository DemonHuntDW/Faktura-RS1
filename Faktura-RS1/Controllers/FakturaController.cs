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
    public class FakturaController : Controller
    {
        MojContext _database = new MojContext();

        public IActionResult Index()
        {
            var m = new FakturaPrikazVM
            {
                rows = _database.Faktura.Select(s => new FakturaPrikazVM.Row
                {
                    Datum = s.Datum,
                    FakturaID = s.Id,
                    Klijent = s.Klijent.ImePrezime
                }).ToList()
            };


            return View(m);
        }


        public IActionResult Dodaj()
        {
            var m = new FakturaDodajVM
            {
                KlijentStavke = _database.Klijent.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.ImePrezime
                }).ToList(),

                PonudaStavke = _database.Ponuda
                    //     .Where(s=>s.FakturaId==null)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Klijent.ImePrezime + " - " + s.Datum.ToString("dd.MM.yyyy")

                    }).ToList(),
                datum = DateTime.Now
            };

            return View(m);
        }

        public IActionResult Snimi(FakturaDodajVM x)
        {
            Faktura f = new Faktura
            {
                Datum = x.datum,
                KlijentId = x.KlijentID
            };
            _database.Faktura.Add(f);

            if (x.PonudaID != null)
            {
                Ponuda p = _database.Ponuda.Find(x.PonudaID);
                p.Faktura = f;

                List<PonudaStavka> ponudaStavke = _database.PonudaStavka.Where(stavka => p.Id == stavka.PonudaId).ToList();

                ponudaStavke.ForEach(s =>
                {
                    _database.Add(
                        new FakturaStavka
                        {
                            Faktura = f,
                            ProizvodId = s.ProizvodId,
                            Kolicina = s.Kolicina,
                            PopustProcenat = 5 ,
                            
                        }
                    );
                });
            }

            _database.SaveChanges();

            return Redirect("/Faktura/Index");
        }

        public IActionResult Obrisi(int FakturaID)
        {
            Faktura f = _database.Faktura.Find(FakturaID);
            _database.Remove(f);

            var obrisati = _database.FakturaStavka.Where(s => s.FakturaId == f.Id).ToList();
            _database.RemoveRange(obrisati);

            List<Ponuda> ponude = _database.Ponuda.Where(s => s.FakturaId == f.Id).ToList();
            ponude.ForEach(s => { s.FakturaId = null; });

            _database.SaveChanges();

            return Redirect("/Faktura/Index");
        }

        public IActionResult Detalji(int FakturaID)
        {
            FakturaDetaljiVM m = _database.Faktura
                .Where(s => s.Id == FakturaID)
                .Select(s => new FakturaDetaljiVM
                {
                    Datum = s.Datum,
                    Klijent = s.Klijent.ImePrezime,
                    FakturaID = s.Id
                }).FirstOrDefault();

            return View(m);
        }
    }
}