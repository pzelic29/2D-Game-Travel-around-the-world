using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace OTTER
{
    public partial class Odabir_igraca : Form
    {
        public Form POCETNA;

        public bool OdaberiDrzavu = false;

        public Odabir_igraca()
        {
            InitializeComponent();
        }
        public string putnik = "putnik1";
        public string imePlayer = "Player";
        public int bodoviPlayer;

        public string datoteka = "highscore.txt";
        private void Form2_Load(object sender, EventArgs e)
        {
            if (!File.Exists(datoteka))
            {
                using (FileStream fs = File.Create(datoteka))
                {
                    Console.WriteLine("File created.");
                }
            }
            using (StreamReader sr = File.OpenText(datoteka))
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.putnik = "putnik1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.putnik = "putnik2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.putnik = "putnik3";
        }
        public string tekst;
        private void button2_Click(object sender, EventArgs e)
        {
            OdaberiDrzavu = true;
            if (textBox1.Text != "")
            {
                imePlayer = textBox1.Text;
                bodoviPlayer = 0;
            }
            if (listBox1.SelectedIndex > -1)
            {
                tekst = listBox1.SelectedItem.ToString();
                var splitString = tekst.Split(' ');
                imePlayer = splitString[0];
                bodoviPlayer = int.Parse(splitString[1]);

            }
            //var karta = new Odabir_drzave(putnik, imePlayer, bodoviPlayer);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label5.Text = "Krećeš se pomoću tipki AWD\nHvatajući znamenitosti karakteristične za državu u kojoj se trenutno nalaziš dobivaš bodove \nOdređeni broj bodova te vodi u novu državu.\nAko uhvatiš znamenitosti koje ne pripadaju toj državi gubiš život,ali i bodove.\nIgra je gotova kad izgubiš sve živote.";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label5.Text = "";
        }
    }
}
