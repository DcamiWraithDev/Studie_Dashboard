using Database.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Database.Data
{
    public class Cursussen
    {
        private readonly string _connectionString;

        public Cursussen(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        // Return table-only cursussen
        public List<Cursus> GetCursussen(string statusFilter = "All", string typeFilter = "All", string blokFilter = "All")
        {
            List<Cursus> cursussen = new List<Cursus>();
            int statusId = MapStatusSymbolToId(statusFilter);
            int typeId = MapTypeSymbolToId(typeFilter);

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = @"
                    SELECT c.Cursus_ID, c.Cursus_Naam, c.Datum, c.Studie_Punten,
                           c.Cursus_Status, c.Cursus_Type, c.Studie_Blok
                    FROM cursussen c
                    WHERE (@StatusID = 0 OR c.Cursus_Status = @StatusID)
                      AND (@TypeID = 0 OR c.Cursus_Type = @TypeID)
                      AND (@BlokID = 0 OR c.Studie_Blok = @BlokID);";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StatusID", statusId);
                    cmd.Parameters.AddWithValue("@TypeID", typeId);
                    int blokId = blokFilter == "All" ? 0 : GetBlokIdByName(blokFilter);
                    cmd.Parameters.AddWithValue("@BlokID", blokId);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cursussen.Add(new Cursus
                            {
                                Cursus_ID = reader.GetInt32("Cursus_ID"),
                                Cursus_Naam = reader.GetString("Cursus_Naam"),
                                Datum = reader.GetDateTime("Datum"),
                                Studie_Punten = reader.GetInt32("Studie_Punten"),
                                Cursus_Status = reader.GetInt32("Cursus_Status"),
                                Cursus_Type = reader.GetInt32("Cursus_Type"),
                                Studie_Blok = reader.GetInt32("Studie_Blok")
                            });
                        }
                    }
                }
            }
            return cursussen;
        }

        // Get totals for progress bars
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
                    JOIN type t   ON c.Cursus_Type = t.Type_ID;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudiePuntenTotals
                            {
                                MaxFormatief = reader.GetInt32("MaxFormatief"),
                                EarnedFormatief = reader.GetInt32("EarnedFormatief"),
                                MaxSummatief = reader.GetInt32("MaxSummatief"),
                                EarnedSummatief = reader.GetInt32("EarnedSummatief")
                            };
                        }
                    }
                }
            }
            return new StudiePuntenTotals();
        }

        // Get all blok names
        public List<string> GetAllBlokken()
        {
            List<string> blokken = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Blok_Naam FROM blok ORDER BY Blok_ID;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            blokken.Add(reader.GetString("Blok_Naam"));
                        }
                    }
                }
            }
            return blokken;
        }

        private int GetBlokIdByName(string blokName)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Blok_ID FROM blok WHERE Blok_Naam = @BlokNaam LIMIT 1;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BlokNaam", blokName);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        private int MapStatusSymbolToId(string statusSymbol)
        {
            if (statusSymbol == "J" || statusSymbol == "J (Ja)") return 2;
            if (statusSymbol == "N" || statusSymbol == "N (Nee)") return 3;
            if (statusSymbol == "-" || statusSymbol == "- (Niet gestart)") return 1;
            return 0;
        }

        private int MapTypeSymbolToId(string typeSymbol)
        {
            if (typeSymbol == "Formatief") return 1;
            if (typeSymbol == "Summatief") return 2;
            return 0;
        }
    }
}
