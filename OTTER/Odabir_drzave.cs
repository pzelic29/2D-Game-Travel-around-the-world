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
    public partial class Odabir_drzave : Form
    {
        public Form IGRAC;
        string _putnik;
        string _imeigraca;
        int _bodoviigraca;
        public Odabir_drzave(string putnik,string imeigraca,int bodoviigraca)
        {
            this._putnik = putnik;
            this._imeigraca = imeigraca;
            this._bodoviigraca=bodoviigraca;
            InitializeComponent();
        }


        public bool StartIgre = false;

        public Odabir_drzave()
        {
            // TODO: Complete member initialization
        }
        public string countryname;
        private void button2_Click(object sender, EventArgs e)
        {
            countryname = "Italija";
            StartIgre = true;
            this.Close();
        }

        private void buttonSpanjolska_Click(object sender, EventArgs e)
        {
            countryname = "Spanjolska";
            StartIgre = true;
            this.Close();
        }

        private void buttonGrcka_Click(object sender, EventArgs e)
        {
            countryname = "Grcka";
            StartIgre = true;
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnNatrag_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSpanjolska_MouseEnter(object sender, EventArgs e)
        {
            label1.Text = "Crkva Sagrada Familia gradi se više od 100 godina!";
        }

        private void buttonSpanjolska_Leave(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void buttonItalija_MouseEnter(object sender, EventArgs e)
        {
            label1.Text = "Po zakonu sve gondole u Italiji trebaju biti crne boje!";
        }

        private void buttonItalija_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void buttonGrcka_MouseEnter(object sender, EventArgs e)
        {
            label1.Text = "Grčka hrana danas je gotovo identična onoj od prije 2000 godina!";
        }

        private void buttonGrcka_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "";
        }

    }
}
