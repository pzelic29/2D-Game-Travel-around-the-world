using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public abstract class Likovi : Sprite
    {
        protected int zivoti;
        protected int brzina;
        protected string ime;
        public Likovi(string path, int xcor, int ycor)
            : base(path, xcor, ycor)
        {
            this.ime = "Player";
            this.zivoti = 5;
            this.brzina = 5;
        }

        public int Zivoti
        {
            get { return zivoti; }
            set { zivoti = value; }
        }
        public int Brzina
        {
            get { return brzina; }
            set { brzina = value; }
        }
        public string Ime { get; set; }
    }
    public class MagicniNapitak : Sprite
    {
        public MagicniNapitak(string path, int xcor, int ycor)
            : base(path, xcor, ycor)
        {
        }
    }
    public class Lik : Likovi
    {
        private int brojacTocnih, brojacNetocnih;
        public int BrojacTocnih { get; set; }
        public int BrojacNetocnih { get; set; }
        private int bodovi;
        private int maxBodovi;//broj bodova za prelazak u novu drzavu;
        private int visina_Skoka;
        public Lik(string path, int xcor, int ycor)
            : base(path, xcor, ycor)
        {
            this.zivoti = 3;
            this.brzina = 7;
            this.bodovi = 0;
            this.maxBodovi = 0;
            this.visina_Skoka = 7;
            this.brojacNetocnih = 0;
            this.brojacTocnih = 0;
        }
        public int Zivoti
        {
            get { return zivoti; }
            set
            {
                zivoti = value;
            }
        }

        public int Bodovi { get; set; }

        public int MaxBodovi
        {
            get { return maxBodovi; }
            set { maxBodovi = value; }
        }
        public int Visina_Skoka
        {
            get { return visina_Skoka; }
            set { visina_Skoka = value; }
        }
        public override int X  //ne dopustamo da izade van granica
        {
            get
            {
                return this.x;
            }
            set
            {
                if (value < GameOptions.LeftEdge)
                {
                    throw new ArgumentException();
                }
                else if (value > GameOptions.RightEdge - this.Width)
                {
                    x = GameOptions.RightEdge - this.Width;
                }
                else
                {
                    x = value;
                }
            }
        }
    }
    public class Znamenitosti : Likovi
    {
        protected int bodoviZnam;
        public Znamenitosti(string path, int xcor, int ycor, int bodoviZnamenitosti = 10, int brzinaz = 6)
            : base(path, xcor, ycor)
        {
            this.bodoviZnam = bodoviZnamenitosti;
            this.brzina = brzinaz;

        }
        public int BodoviZnam
        {
            get { return bodoviZnam; }
            set
            {
                bodoviZnam = value;
            }
        }
        public override int Y
        {
            get { return base.Y; }
            set
            {
                if (value > GameOptions.DownEdge)
                {
                    this.y = 0;
                    this.X = generator.Next(GameOptions.LeftEdge, GameOptions.RightEdge-this.Width);
                }
                else
                {
                    base.Y = value;
                }
                    
            }
        }
        Random generator = new Random();

    }
}
