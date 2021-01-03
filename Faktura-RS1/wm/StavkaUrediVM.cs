using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Faktura.ViewModels
{
    public class StavkaUrediVM
    {
        public int ProizvodID { get; set; }
        public List<SelectListItem> ProizvodStavke { get; set; }
        public float Kolicina { get; set; }
        public int StavkaID { get; set; }
        public int FakturaID { get; set; }
    }
}
