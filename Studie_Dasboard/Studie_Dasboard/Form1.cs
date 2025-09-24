using Database;
using Database.Data;
using Database.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Studie_Dasboard
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new BindingSource();
        private string connString = "Server=localhost;Port=3306;Database=dashboard_db;User Id=root;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup Status ComboBox
            cBox.Items.Clear();
            cBox.Items.AddRange(new string[] { "Alle Statusen", "J (Ja)", "N (Nee)", "- (Niet gestart)" });
            cBox.SelectedIndex = 0;
            cBox.SelectedIndexChanged += FilterComboBoxes;

            // Setup Type ComboBox
            typeBox.Items.Clear();
            typeBox.Items.AddRange(new string[] { "Alle Types", "Formatief", "Summatief" });
            typeBox.SelectedIndex = 0;
            typeBox.SelectedIndexChanged += FilterComboBoxes;

            blokBox.Items.Clear();
            blokBox.Items.Add("Alle Blokken");

            cursussen cursussenDb = new cursussen(connString);
            var blokken = cursussenDb.GetAllBlokken();
            foreach (var blok in blokken)
            {
                blokBox.Items.Add(blok);
            }

            blokBox.SelectedIndex = 0;
            blokBox.SelectedIndexChanged += FilterComboBoxes;

            // Bind DataGridView initially with all rows
            RefreshGrid("Alle Statusen", "Alle Types", "Alle Blokken");

            dGrid.Columns["Cursus_ID"].Visible = false;
            dGrid.Columns["Cursus_Type"].Visible = false;
        }

        private void FilterComboBoxes(object sender, EventArgs e)
        {
            string selectedStatus = cBox.SelectedItem.ToString();
            string selectedType = typeBox.SelectedItem.ToString();
            string selectedBlok = blokBox.SelectedItem.ToString();

            RefreshGrid(selectedStatus, selectedType, selectedBlok);
        }


        // Helper method to refresh the grid from database
        private void RefreshGrid(string status, string type, string blok)
        {
            cursussen cursussenDb = new cursussen(connString);
            var filtered = cursussenDb.GetDataByFilter(status, type, blok);

            bs.DataSource = new BindingList<Cursus>(filtered);
            dGrid.DataSource = bs;

            // get totals for ALL courses (not filtered)
            var totals = cursussenDb.GetStudiepuntenTotalsAll();

            // Update labels
            plabelSubjectief.Text = $"{totals.EarnedSummatief} / {totals.MaxSummatief}";
            pLabelobjectief.Text = $"{totals.EarnedFormatief} / {totals.MaxFormatief}";

            // Update progress bars
            if (totals.MaxSummatief > 0)
            {
                pBarSummatief.Maximum = totals.MaxSummatief;
                pBarSummatief.Value = Math.Min(totals.EarnedSummatief, totals.MaxSummatief);
            }
            else
            {
                pBarSummatief.Maximum = 1; // avoid zero-division errors
                pBarSummatief.Value = 0;
            }

            if (totals.MaxFormatief > 0)
            {
                pBarFormatief.Maximum = totals.MaxFormatief;
                pBarFormatief.Value = Math.Min(totals.EarnedFormatief, totals.MaxFormatief);
            }
            else
            {
                pBarFormatief.Maximum = 1;
                pBarFormatief.Value = 0;
            }
        }

    }
}
