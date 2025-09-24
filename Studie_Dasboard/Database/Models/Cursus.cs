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
        public int Studie_Punten {  get; set; }
        public string Blok_Naam { get; set; }
        public string Cursus_Status { get; set; }
        public int Cursus_Type { get; set; }
        public string Cursus_TypeNaam { get; set; }

    }

}
