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
                string query = @"SELECT * FROM cursussen";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Cursus cursus = new Cursus
                            {
                                Cursus_ID = r.GetInt32("Cursus_ID"),
                                Cursus_Naam = r.GetString("Cursus_Naam"),
                                Datum = r.GetDateTime("Datum").Date
                            };

                            Console.WriteLine(cursus.Cursus_Naam);
                            Console.WriteLine(cursus.Cursus_ID);
                            Console.WriteLine(cursus.Datum.ToString("dd-MM-yyyy"));

                        }
                    }
                }


                return cursussen;
            }
        }
    }
}
