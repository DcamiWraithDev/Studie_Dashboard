using Database.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Data
{
    public class cursussen
    {
        private string _connectionString;

        public cursussen(string connectionString) 
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }


        public List<Cursus> GetData()
        {
            List<Cursus> cursussen = new List<Cursus>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = @"
                SELECT c.Cursus_ID, 
                        c.Cursus_Naam, 
                        c.Datum, 
                        b.Blok_Naam, 
                        s.Status_ID,
                        t.Type_Naam AS Cursus_Type
                FROM cursussen c
                JOIN blok b   ON c.Studie_Blok   = b.Blok_ID
                JOIN status s ON c.Cursus_Status = s.Status_ID
                JOIN type t   ON c.Cursus_Type   = t.Type_ID;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int statusId = r.GetInt32("Status_ID");
                            string statusSymbol;
                            if (statusId == 1)
                                statusSymbol = "-"; // Cursussen nog te volgen
                            else if (statusId == 2)
                                statusSymbol = "J"; // Cursussen behaald
                            else if (statusId == 3)
                                statusSymbol = "N"; // Cursussen niet behaald
                            else
                                statusSymbol = "?";

                            cursussen.Add(new Cursus
                            {
                                Cursus_ID = r.GetInt32("Cursus_ID"),
                                Cursus_Naam = r.GetString("Cursus_Naam"),
                                Datum = r.GetDateTime("Datum").Date,
                                Blok_Naam = r.GetString("Blok_Naam"),
                                Cursus_Status = statusSymbol,
                            });
                        }
                    }
                }
            }

            return cursussen;
        }

    }
}
