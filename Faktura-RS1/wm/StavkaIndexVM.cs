using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Faktura.ViewModels
{
    public class StavkaIndexVM
    {
        public class Row
        {
            public int StavkaID { get; set; }
            public string NazivProizvoda{ get; set; }
            public float Kolicina{ get; set; }
            public float Popust{ get; set; }
            public double Iznos{ get; set; }
            public float Cijena { get; set; }
        }

        public List<Row> rows { get; set; }
        public int FakturaID { get; set; }
    }
}
