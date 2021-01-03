using System;

namespace RS1_Faktura.Models
{
    public class FakturaDetaljiVM
    {
        public DateTime Datum { get; set; }
        public string Klijent { get; set; }
        public int FakturaID { get; set; }
    }
}