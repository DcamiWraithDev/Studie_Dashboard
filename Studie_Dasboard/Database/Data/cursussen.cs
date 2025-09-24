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
                            c.Studie_Punten,
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
                                Studie_Punten = r.GetInt32("Studie_Punten"),
                                Blok_Naam = r.GetString("Blok_Naam"),
                                Cursus_Status = statusSymbol,
                            });
                        }
                    }
                }
            }

            return cursussen;
        }

        public List<Cursus> GetDataByFilter(string statusSymbol, string typeSymbol, string blokNaam)
        {
            List<Cursus> cursussen = new List<Cursus>();

            int statusId = MapStatusSymbolToId(statusSymbol);
            int typeId = MapTypeSymbolToId(typeSymbol);

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = @"
            SELECT c.Cursus_ID, 
                   c.Cursus_Naam, 
                   c.Datum, 
                   c.Studie_Punten,
                   b.Blok_Naam, 
                   s.Status_ID,
                   t.Type_ID AS Cursus_Type,
                   t.Type_Naam
            FROM cursussen c
            JOIN blok b   ON c.Studie_Blok   = b.Blok_ID
            JOIN status s ON c.Cursus_Status = s.Status_ID
            JOIN type t   ON c.Cursus_Type   = t.Type_ID
            WHERE (@StatusID = 0 OR c.Cursus_Status = @StatusID)
              AND (@TypeID = 0 OR c.Cursus_Type = @TypeID)
              AND (@BlokNaam = 'Alle Blokken' OR b.Blok_Naam = @BlokNaam);";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StatusID", statusId);
                    cmd.Parameters.AddWithValue("@TypeID", typeId);
                    cmd.Parameters.AddWithValue("@BlokNaam", blokNaam);

                    conn.Open();
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int id = r.GetInt32("Status_ID");
                            string status = "?";
                            if (id == 1) status = "-";
                            else if (id == 2) status = "J";
                            else if (id == 3) status = "N";

                            cursussen.Add(new Cursus
                            {
                                Cursus_ID = r.GetInt32("Cursus_ID"),
                                Cursus_Naam = r.GetString("Cursus_Naam"),
                                Datum = r.GetDateTime("Datum").Date,
                                Studie_Punten = r.GetInt32("Studie_Punten"),
                                Blok_Naam = r.GetString("Blok_Naam"),
                                Cursus_Status = status,
                                Cursus_Type = r.GetInt32("Cursus_Type"),
                                Cursus_TypeNaam = r.GetString("Type_Naam")
                            });
                        }
                    }
                }
            }

            return cursussen;
        }

        public List<string> GetAllBlokken()
        {
            var blokken = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Blok_Naam FROM blok ORDER BY Blok_ID;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            blokken.Add(r.GetString("Blok_Naam"));
                        }
                    }
                }
            }

            return blokken;
        }

        public StudiePuntenTotals GetStudiepuntenTotalsAll()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = @"
        SELECT 
            COALESCE(SUM(CASE WHEN t.Type_Naam = 'Formatief' THEN c.Studie_Punten ELSE 0 END),0) AS MaxFormatief,
            COALESCE(SUM(CASE WHEN t.Type_Naam = 'Formatief' AND s.Status_ID = 2 THEN c.Studie_Punten ELSE 0 END),0) AS EarnedFormatief,
            COALESCE(SUM(CASE WHEN t.Type_Naam = 'Summatief' THEN c.Studie_Punten ELSE 0 END),0) AS MaxSummatief,
            COALESCE(SUM(CASE WHEN t.Type_Naam = 'Summatief' AND s.Status_ID = 2 THEN c.Studie_Punten ELSE 0 END),0) AS EarnedSummatief
        FROM cursussen c
        JOIN status s ON c.Cursus_Status = s.Status_ID
        JOIN type t   ON c.Cursus_Type = t.Type_ID;
        ";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new StudiePuntenTotals
                            {
                                MaxFormatief = r.GetInt32("MaxFormatief"),
                                EarnedFormatief = r.GetInt32("EarnedFormatief"),
                                MaxSummatief = r.GetInt32("MaxSummatief"),
                                EarnedSummatief = r.GetInt32("EarnedSummatief")
                            };
                        }
                    }
                }
            }

            return new StudiePuntenTotals(); // fallback to zeros
        }


        // helpers to map UI combo text -> DB IDs (robust to "Alle ...", "J (Ja)", etc.)
        private int MapStatusSymbolToId(string statusSymbol)
        {
            if (string.IsNullOrWhiteSpace(statusSymbol))
                return 0;

            statusSymbol = statusSymbol.Trim();

            // If the item contains the literal 'J' or 'Ja' -> passed
            if (statusSymbol.StartsWith("J") || statusSymbol.Contains("Ja"))
                return 2;
            // If it contains 'N' or 'Nee' -> not passed
            if (statusSymbol.StartsWith("N") || statusSymbol.Contains("Nee"))
                return 3;
            // If it contains '-' or 'Niet' -> not started
            if (statusSymbol.StartsWith("-") || statusSymbol.Contains("Niet"))
                return 1;

            // default = 0 -> means "all statuses"
            return 0;
        }

        private int MapTypeSymbolToId(string typeSymbol)
        {
            if (string.IsNullOrWhiteSpace(typeSymbol))
                return 0;

            typeSymbol = typeSymbol.Trim();

            if (typeSymbol.IndexOf("Formatief", StringComparison.OrdinalIgnoreCase) >= 0)
                return 1;
            if (typeSymbol.IndexOf("Summatief", StringComparison.OrdinalIgnoreCase) >= 0)
                return 2;

            // default = 0 -> means "all types"
            return 0;
        }

    }
}
