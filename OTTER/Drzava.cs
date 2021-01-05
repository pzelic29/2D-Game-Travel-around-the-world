using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Drzava
    {
        protected string imedrzave,pozadinadrzave;
        public string ImeDrzave { get; set; }
        public string PozadinaDrzave { get; set; }
        protected List<Znamenitosti> znamenitosti,neprijatelj,neprijatelj1;
        public List<Znamenitosti> Neprijatelj { get; set; }
        public List<Znamenitosti> Neprijatelj1 { get; set; }
        public  List<Znamenitosti> Znamenitosti { get; set; }

        public Drzava()
        {
            
        }

        public Znamenitosti SlucajneZnamenitost()
        {
            int indeksznam = RandomNumber(0, Znamenitosti.Count - 1);
            return Znamenitosti[indeksznam];
        }

        public Znamenitosti SlucajneLoseZnamenitosti()
        {
            int indekslose = RandomNumber(0, Neprijatelj1.Count - 1);
            return Neprijatelj1[indekslose];
        }
        public Znamenitosti SlucajneLose2()
        {
            int indekslose2 = RandomNumber(0, Neprijatelj.Count - 1);
            return Neprijatelj[indekslose2];
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}
