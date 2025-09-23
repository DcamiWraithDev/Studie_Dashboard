using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database;
using Database.Data;
using Database.Models;

namespace Studie_Dasboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "Server=localhost;Port=3306;Database=dashboard_db;User Id=root;";

            cursussen cursussen = new cursussen(connString);

            List<Cursus> alle_curssusen = cursussen.GetData();

            foreach (Cursus f in alle_curssusen)
            {
                Console.WriteLine(f.Cursus_Naam);
            }
        }
    }
}
