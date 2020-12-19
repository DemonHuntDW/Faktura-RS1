using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Faktura.WM
{
    public class DodajFakturu
    {
        public int klijentID { get; set; }

        public List<SelectListItem> klijentstavke { get; set; }

        public DateTime datum { get; set; }


        public int PonudaID { get; set; }
        public List<SelectListItem> ponudastavke { get; set; }







    }
}
