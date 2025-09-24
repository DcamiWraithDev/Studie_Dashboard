using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Cursus
    {
        public int Cursus_ID { get; set; }
        public string Cursus_Naam { get; set; }
        public DateTime Datum { get; set; }
        public int Studie_Punten { get; set; }
        public int Studie_Blok { get; set; }      // FK naar blok tabel
        public int Cursus_Status { get; set; }    // FK naar status tabel
        public int Cursus_Type { get; set; }      // FK naar type tabel
    }

}
