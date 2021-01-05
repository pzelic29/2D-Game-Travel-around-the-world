using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Kraj : Form
    {
        public Kraj(string poruka)
        {
            InitializeComponent();
            label1.Text = poruka;
        }

        public bool OdaberiIgraca = false;
        public bool OdaberiDrzavu = false;


        public string d = "highscore.txt";
        private void Kraj_Load(object sender, EventArgs e)
        {
            //citanje iz txt datoteke za score
            using (StreamReader sr = File.OpenText(d))
            {
                string linija = sr.ReadLine();
                while (linija != null)
                {
                    string[] niz = linija.Split(' ');
                    string igrac = niz[0];
                    int bodovi = int.Parse(niz[1]);
                    if (bodovi != 0)
                    {
                        listBox1.Items.Add(linija + "\n");
                    }
                    linija = sr.ReadLine();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdaberiDrzavu = true; 
            this.Close();
            this.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OdaberiIgraca = true;
            this.Close();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
    }
}
