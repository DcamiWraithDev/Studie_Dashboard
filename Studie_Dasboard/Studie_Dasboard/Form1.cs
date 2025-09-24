using Database.Data;
using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Studie_Dasboard
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new BindingSource();
        private string connString = "Server=localhost;Port=3306;Database=dashboard_db;User Id=root;";
        private Dictionary<int, string> blokCache; // cache blok names for UI

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup ComboBoxes
            cBox.Items.Clear();
            cBox.Items.AddRange(new string[] { "Alle Statusen", "J (Ja)", "N (Nee)", "- (Niet gestart)" });
            cBox.SelectedIndex = 0;
            cBox.SelectedIndexChanged += FilterComboBoxes;

            typeBox.Items.Clear();
            typeBox.Items.AddRange(new string[] { "Alle Types", "Formatief", "Summatief" });
            typeBox.SelectedIndex = 0;
            typeBox.SelectedIndexChanged += FilterComboBoxes;

            blokBox.Items.Clear();
            blokBox.Items.Add("Alle Blokken");

            // Load blok names from DB once and cache
            var cursussenDb = new Cursussen(connString);
            var blokken = cursussenDb.GetAllBlokken();
            blokCache = new Dictionary<int, string>();
            for (int i = 0; i < blokken.Count; i++)
            {
                int blokId = i + 1; // assuming IDs are sequential
                blokCache[blokId] = blokken[i];
                blokBox.Items.Add(blokken[i]);
            }

            blokBox.SelectedIndex = 0;
            blokBox.SelectedIndexChanged += FilterComboBoxes;

            // Initial binding
            RefreshGrid("Alle Statusen", "Alle Types", "Alle Blokken");

            // Hide ID columns
            if (dGrid.Columns["Cursus_ID"] != null)
                dGrid.Columns["Cursus_ID"].Visible = false;
            if (dGrid.Columns["Cursus_Type"] != null)
                dGrid.Columns["Cursus_Type"].Visible = false;
        }

        private void FilterComboBoxes(object sender, EventArgs e)
        {
            string selectedStatus = cBox.SelectedItem.ToString();
            string selectedType = typeBox.SelectedItem.ToString();
            string selectedBlok = blokBox.SelectedItem.ToString();

            RefreshGrid(selectedStatus, selectedType, selectedBlok);
        }

        private void RefreshGrid(string status, string type, string blok)
        {
            var cursussenDb = new Cursussen(connString);
            var filtered = cursussenDb.GetCursussen(status, type, blok);

            var gridData = new List<object>();

            if (filtered.Count > 0)
            {
                foreach (var c in filtered)
                {
                    gridData.Add(new
                    {
                        c.Cursus_ID,
                        c.Cursus_Naam,
                        c.Datum,
                        c.Studie_Punten,
                        Blok_Naam = GetBlokName(c.Studie_Blok),
                        Cursus_Status = StatusIdToSymbol(c.Cursus_Status),
                        Cursus_TypeNaam = TypeIdToName(c.Cursus_Type)
                    });
                }
            }
            else
            {
                // No courses found, add a placeholder row
                gridData.Add(new
                {
                    Cursus_ID = 0,
                    Cursus_Naam = "Geen cursus / examen gevonden",
                    Datum = DateTime.MinValue,
                    Studie_Punten = 0,
                    Blok_Naam = "-",
                    Cursus_Status = "-",
                    Cursus_TypeNaam = "-"
                });
            }

            bs.DataSource = gridData;
            dGrid.DataSource = bs;

            // Totals for progress bars remain unchanged
            var totals = cursussenDb.GetStudiepuntenTotalsAll();

            plabelSubjectief.Text = string.Format("{0} / {1}", totals.EarnedSummatief, totals.MaxSummatief);
            pLabelobjectief.Text = string.Format("{0} / {1}", totals.EarnedFormatief, totals.MaxFormatief);

            pBarSummatief.Maximum = totals.MaxSummatief > 0 ? totals.MaxSummatief : 1;
            pBarSummatief.Value = Math.Min(totals.EarnedSummatief, pBarSummatief.Maximum);

            pBarFormatief.Maximum = totals.MaxFormatief > 0 ? totals.MaxFormatief : 1;
            pBarFormatief.Value = Math.Min(totals.EarnedFormatief, pBarFormatief.Maximum);
        }


        // Map status ID -> symbol
        private string StatusIdToSymbol(int id)
        {
            switch (id)
            {
                case 1: return "-";
                case 2: return "J";
                case 3: return "N";
                default: return "?";
            }
        }

        // Map type ID -> name
        private string TypeIdToName(int id)
        {
            switch (id)
            {
                case 1: return "Formatief";
                case 2: return "Summatief";
                default: return "Onbekend";
            }
        }

        // Get blok name from cached dictionary
        private string GetBlokName(int blokId)
        {
            if (blokCache != null && blokCache.ContainsKey(blokId))
                return blokCache[blokId];
            return "Onbekend";
        }
    }
}
