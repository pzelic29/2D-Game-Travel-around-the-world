using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Pocetna : Form
    {
        public Pocetna()
        {
            InitializeComponent();
        }

        private string _putnik;
        private string _imePlayer;
        private int _bodoviPlayer;
        private int zivoti;
        private void Igra(bool odabirIgraca)
        {
            if (odabirIgraca)
            {
                Odabir_igraca odaberiIgraca = new Odabir_igraca();
                odaberiIgraca.ShowDialog();

                _putnik = odaberiIgraca.putnik;
                _imePlayer = odaberiIgraca.imePlayer;
                _bodoviPlayer = odaberiIgraca.bodoviPlayer;
                if (!odaberiIgraca.OdaberiDrzavu)
                {
                    return;
                }
            }
            OdaberiDrzavu(_putnik, _imePlayer, _bodoviPlayer);
        }

        private void OdaberiDrzavu(string putnik, string imePlayer, int bodoviPlayer)
        {
            Odabir_drzave odaberiDrzavu = new Odabir_drzave(putnik, imePlayer, bodoviPlayer);
            odaberiDrzavu.ShowDialog();
            if (odaberiDrzavu.StartIgre)
            {
                BGL bgl = new BGL(imePlayer, _putnik, odaberiDrzavu.countryname, bodoviPlayer,zivoti);
                BGL.allSprites.Clear();
                bgl.ShowDialog();

                if (bgl.OdabirDrzave)
                {
                    _bodoviPlayer = bgl.Bodovi;
                    Igra(false);
                }
                if (bgl.Kraj)
                {
                    Kraj kraj = new Kraj("Tvoj godišnji odmor je gotov!");
                    kraj.ShowDialog();
                    if (kraj.OdaberiIgraca)
                    {
                        Igra(true);
                    }
                    if (kraj.OdaberiDrzavu)
                    {
                        _bodoviPlayer = bgl.Bodovi;
                        Igra(false);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Igra(true);
            if (!this.IsDisposed)
            {
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
