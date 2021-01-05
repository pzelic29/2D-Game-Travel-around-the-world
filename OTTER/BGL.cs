using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using WMPLib;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        public int Bodovi;
        public bool Kraj = false;
        public bool OdabirDrzave = false;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {
                if (allSprites != null && allSprites.Any())
                {
                    foreach (Sprite sprite in allSprites)
                    {
                        if (sprite != null)
                            if (sprite.Show == true)
                            {
                                g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                            }
                        if (allSprites.Change)
                            break;
                    }
                    if (allSprites.Change)
                        allSprites.Change = false;
                }
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }
        //public SoundPlayer glazba;
        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();


            //if (_imeDrz == "Italija")
            //{
            //    glazba = new SoundPlayer("music.mp3");
            //    glazba.Play();
            //}
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.Transparent);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        public BGL(string imeigraca, string putnik, string imeDrz, int bodoviigraca,int zivoti)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._imeigraca = imeigraca;
            this._putnik = putnik;
            this._imeDrz = imeDrz;
            this._BodoviIgraca = bodoviigraca;
            this._zivoti= zivoti;
            //START = true;
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */
        private string _imeigraca, _putnik, _imeDrz;
        private int _BodoviIgraca,_zivoti, broj = 250;//broj -početna koordinata magičnog napitka
        public bool ItalijaON, SpanjolskaON, GrckaON;
        Lik lik;
        MagicniNapitak napitak;
        Znamenitosti dobar, los1, los2;  // Likovi koji padaju
        Drzava aktualnaDrzava, Italija, Grcka, Spanjolska;
        List<Znamenitosti> znamenitostiGrcke, znamenitostiSpanjolske, znamenitostiItalije;

        /* Initialization */
        public delegate void TouchHandler();
        public static event TouchHandler DodirZnamenitost;
        //Metoda za event
        private void DodirZnam()
        {
            if (lik.TouchingSprite(los1))
            {
                los1.Y = 0;
            }
            if (lik.TouchingSprite(los2))
            {
                los2.Y = 0;
            }
            if (lik.TouchingSprite(dobar))
            {
                dobar.Y = 0;
            }
        }
        private void SetupGame()
        {
            //1. setup stage
            SetStageTitle("PMF");
            setBackgroundColor(Color.WhiteSmoke);
            //ako je igrač izgubio i želi ponovno igrati trebamo start postavit ponovno na true
            START = true;

            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");

            //2. add sprites
            if (_putnik == "putnik1")
            {
                lik = new Lik("sprites\\lik1.png", 340, 440);
            }
            else if (_putnik == "putnik2")
            {
                lik = new Lik("sprites\\lik2.png", 340, 440);
            }
            else if (_putnik == "putnik3")
            {
                lik = new Lik("sprites\\lik3.png", 340, 440);
            }
            Game.AddSprite(lik);
            lik.RotationStyle = "AllAround";
            lik.Bodovi = _BodoviIgraca;
            lik.Ime = _imeigraca;
            lik.SetSize(90);

            napitak = new MagicniNapitak("sprites\\napitak.png", 0, 400);
            napitak.SetSize(55);
            napitak.SetVisible(false);


            Game.AddSprite(napitak);

            //Znamenitosti Grcke
            znamenitostiGrcke = new List<Znamenitosti>();
            znamenitostiGrcke.Add(new Znamenitosti("sprites\\akropola_grcka.png", 0, 0));
            znamenitostiGrcke.Add(new Znamenitosti("sprites\\brod_grcka.png", 0, 0));
            znamenitostiGrcke.Add(new Znamenitosti("sprites\\minotaur_grcka.png", 0, 0, 40, 7));
            znamenitostiGrcke.Add(new Znamenitosti("sprites\\olimpijskeigre_grcka.png", 0, 0));
            znamenitostiGrcke.Add(new Znamenitosti("sprites\\vaza_grcka.png", 0, 0));

            //Znamenitosti Spanjolske
            znamenitostiSpanjolske = new List<Znamenitosti>();
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\casabatillo_spanjolska.png", 0, 0));
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\flamenco_spanjolska.png", 0, 0));
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\korida_spanjolska.png", 0, 0));
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\sagrada_familija_spanjolska.png", 0, 0));
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\salvadordali.png", 0, 0));
            znamenitostiSpanjolske.Add(new Znamenitosti("sprites\\vjetrenjaca_spanjolska.png", 0, 0));

            //Znamenitosti Italije
            znamenitostiItalije = new List<Znamenitosti>();
            znamenitostiItalije.Add(new Znamenitosti("sprites\\dante_italija.png", 0, 0, 30, 5));
            znamenitostiItalije.Add(new Znamenitosti("sprites\\firenza_italija.png", 0, 0));
            znamenitostiItalije.Add(new Znamenitosti("sprites\\pisa_italija.png", 0, 0));
            znamenitostiItalije.Add(new Znamenitosti("sprites\\Rome_italija.png", 0, 0));
            znamenitostiItalije.Add(new Znamenitosti("sprites\\venecija_italija.png", 0, 0));

            if (_imeDrz == "Italija")
            {
                UcitajItaliju();
                ItalijaON = true;
                SpanjolskaON = false;
                GrckaON = false;

            }
            else if (_imeDrz == "Spanjolska")
            {
                UcitajSpanjolsku();
                ItalijaON = false;
                SpanjolskaON = true;
                GrckaON = false;


            }
            else if (_imeDrz == "Grcka")
            {
                UcitajGrcku();
                ItalijaON = false;
                SpanjolskaON = false;
                GrckaON = true;

            }
            //Povezivanje događaja i metoda
            DodirZnamenitost += DodirZnam;

            // Instanciranje likova koji padaju
            los1 = new Znamenitosti("sprites\\akropola_grcka.png", 0, 0);
            los1.SetSize(70);
            los2 = new Znamenitosti("sprites\\vaza_grcka.png", 400, 0);
            los2.SetSize(70);
            dobar = new Znamenitosti("sprites\\dante_italija.png", 150, 0);
            dobar.SetSize(70);

            //3. scripts that start
            Game.StartScript(Kretanjeputnika);
            Game.StartScript(KretanjeZnamenitosti);

        }
        //Metode za učitavanje drzave
        private void UcitajItaliju()
        {

            Italija = new Drzava()
            {
                ImeDrzave = "Italija",
                PozadinaDrzave = "backgrounds\\italija_pozadina.jpg",
                Znamenitosti = znamenitostiItalije,
                Neprijatelj = znamenitostiGrcke,
                Neprijatelj1 = znamenitostiSpanjolske
            };
            setBackgroundPicture(Italija.PozadinaDrzave);
        }

        private void UcitajSpanjolsku()
        {
            Spanjolska = new Drzava()
            {
                ImeDrzave = "Spanjolska",
                PozadinaDrzave = "backgrounds\\Park-Guell.png",
                Znamenitosti = znamenitostiSpanjolske,
                Neprijatelj = znamenitostiItalije,
                Neprijatelj1 = znamenitostiGrcke
            };

            setBackgroundPicture(Spanjolska.PozadinaDrzave);
        }

        private void UcitajGrcku()
        {
            Grcka = new Drzava()
            {
                ImeDrzave = "Grcka",
                PozadinaDrzave = "backgrounds\\grcka_pozadina.jpg",
                Znamenitosti = znamenitostiGrcke,
                Neprijatelj = znamenitostiItalije,
                Neprijatelj1 = znamenitostiSpanjolske

            };
            setBackgroundPicture(Grcka.PozadinaDrzave);
        }

        /* Scripts */
        private int Kretanjeputnika()
        {
            //postavljamo lika na sredinu
            lik.X = (GameOptions.RightEdge - lik.Width) / 2;
            while (START)
            {
                if (sensing.KeyPressed("D"))
                {
                    lik.X += lik.Brzina;
                    Wait(0.02);
                }
                if (sensing.KeyPressed("A"))
                {

                    try
                    {
                        lik.X -= lik.Brzina;
                    }
                    catch(ArgumentException)
                    {

                        lik.X = GameOptions.LeftEdge;
                    }
                    Wait(0.02);
                }

                if (sensing.KeyPressed("W"))
                {
                    //lik se penje dok nam je broj manji od visine skoka koja je definirana u klasi
                    int br = 0;
                    while (br < lik.Visina_Skoka)
                    {
                        lik.Y -= 5;
                        Wait(0.02);
                        br++;
                        if (sensing.KeyPressed("D"))
                        {
                            lik.X += lik.Brzina;
                        }
                        if (sensing.KeyPressed("A"))
                        {
                            lik.X -= lik.Brzina;
                        }
                    }
                    //lik pada
                    while (br > 0)
                    {

                        lik.Y += 5;
                        Wait(0.02);
                        br--;
                        //da se može kretati u skoku
                        if (sensing.KeyPressed("D"))
                        {
                            lik.X += lik.Brzina;
                        }
                        if (sensing.KeyPressed("A"))
                        {
                            lik.X -= lik.Brzina;
                        }
                    }
                }
            }

            return 0;

        }
        Random rn = new Random();
        //pomoćne varijable 
        private bool dodirlos2; 
        private bool dodirlos1;
        private int KretanjeZnamenitosti()
        {
            //stvaramo instancu države da znamo tko su neprijatelji
            aktualnaDrzava = new Drzava();
            Game.AddSprite(dobar);
            Game.AddSprite(los1);
            Game.AddSprite(los2);
            //ItalijaON,GrckaON,SpanjolskaON bool koji se postavlja na true u metodama za učitavanje država 
            if (ItalijaON)
            {
                aktualnaDrzava = Italija;
            }
            if (GrckaON)
            {
                aktualnaDrzava = Grcka;
            }
            if (SpanjolskaON)
            {
                aktualnaDrzava = Spanjolska;
            }
            //postavljamo trenutni kostim lika iz liste dobrih/loših
            los1.CurrentCostume = aktualnaDrzava.SlucajneLoseZnamenitosti().CurrentCostume;
            los2.CurrentCostume = aktualnaDrzava.SlucajneLose2().CurrentCostume;
            dobar.CurrentCostume = aktualnaDrzava.SlucajneZnamenitost().CurrentCostume;
            ISPIS = String.Format("Lokacija: {0} Igrac: {1} Bodovi= {2} Zivoti={3} Level up:{4}/100%", _imeDrz, lik.Ime, lik.Bodovi, lik.Zivoti, lik.MaxBodovi);
            while (START)
            {

                los1.Y += 6;
                los2.Y += 6;
                dobar.Y += 6;
                Wait(0.1);
                if (los1.TouchingSprite(lik))
                {
                    dodirlos1 = true;
                    ProvjeraZnamenitostiLose(los1, dodirlos1);
                }
                else if (los2.TouchingSprite(lik))
                {
                    dodirlos2 = true;
                    ProvjeraZnamenitostiLose(los2,dodirlos2);

                }
                else if (dobar.TouchingSprite(lik))
                {
                    
                    ProvjeraZnamentiostiDobar();
                }
                ISPIS = String.Format("Lokacija: {0} Igrac: {1} Bodovi= {2} Zivoti={3} Level up:{4}/100%", _imeDrz, lik.Ime, lik.Bodovi, lik.Zivoti, lik.MaxBodovi);
                LikZivoti();
                Provjera();
            }

            return 0;
        }

        private void ProvjeraZnamentiostiDobar()
        {
            DodirZnamenitost.Invoke();
            lik.Bodovi += dobar.BodoviZnam;
            lik.MaxBodovi += dobar.BodoviZnam;
            lik.BrojacTocnih += 1;
            var slucajnaZnamenitost = aktualnaDrzava.SlucajneZnamenitost();
            dobar.SetX(rn.Next(0, GameOptions.RightEdge - dobar.Width));
            dobar.CurrentCostume = slucajnaZnamenitost.CurrentCostume;
            dobar.BodoviZnam = slucajnaZnamenitost.BodoviZnam;
        }
        private void ProvjeraZnamenitostiLose(Znamenitosti losi,bool dl)
        {
            DodirZnamenitost.Invoke();
            lik.Bodovi -= losi.BodoviZnam;
            lik.MaxBodovi -= losi.BodoviZnam;
            lik.BrojacNetocnih += 1;
            losi.SetX(rn.Next(0, GameOptions.RightEdge - los2.Width));
            if (dl==dodirlos2)
            {
                var slucajnaZnamenitost = aktualnaDrzava.SlucajneLose2();
                losi.CurrentCostume = slucajnaZnamenitost.CurrentCostume;
                losi.BodoviZnam = slucajnaZnamenitost.BodoviZnam;
                dodirlos2 = false;              
            }
            else if (dl==dodirlos1)
            {
                var slucajnaZnamenitost = aktualnaDrzava.SlucajneLoseZnamenitosti();
                losi.CurrentCostume = slucajnaZnamenitost.CurrentCostume;
                losi.BodoviZnam = slucajnaZnamenitost.BodoviZnam;
                dodirlos1 = false;
            }
        }

        private void LikZivoti()
        {
            if (lik.BrojacTocnih % 3 == 0 && lik.BrojacTocnih != 0)
            {
                lik.Zivoti += 1;
                lik.BrojacTocnih = 0;
            }
            if (lik.BrojacNetocnih== 1)
            {
                lik.Zivoti -= 1;
                lik.BrojacNetocnih = 0;
            }
            if (lik.Zivoti == 1)
            {
                //pokazujemo magični napitak koji povećava živote
                napitak.SetVisible(true);
                napitak.SetX(broj);
                if (lik.TouchingSprite(napitak))
                {
                    lik.Zivoti += 1;
                    napitak.SetX(GameOptions.RightEdge - napitak.Width);
                    broj = rn.Next(0, GameOptions.RightEdge - napitak.Width);
                    napitak.SetVisible(false);

                }
            }

            //lik.MaxBodovi=broj bodova koji mu omogućuju prelazak na novi level i otvaranje forme Odabir_države
            if (lik.MaxBodovi % 100==0 && lik.MaxBodovi!=0 ||lik.MaxBodovi>=100)
            {
                //vratiti se na formu odabir drzave
                lik.MaxBodovi = 0;
                START = false;
                allSprites.Clear();
                GC.Collect();
                Wait(0.1);
                bool message=true;
                var poruka = MessageBox.Show("Level up! Možeš putovovati u novu državu!");
                while (message)
                {
                    if (poruka != DialogResult.OK)
                    {
                        poruka = MessageBox.Show("Level up! Možeš putovati u novu državu!");
                    }
                    if (poruka == DialogResult.OK)
                    {
                        message = false;
                    }
                }
                //glazba.Stop();
                Bodovi = lik.Bodovi;
                OdabirDrzave = true;
                this.Invoke((Action)delegate { this.Close(); });
            }

            ISPIS = String.Format("Lokacija: {0} Igrac: {1} Bodovi= {2} Zivoti={3} Level up:{4}/100%", _imeDrz, lik.Ime, lik.Bodovi, lik.Zivoti,lik.MaxBodovi);

            if (lik.Zivoti == 0)
            {
                using (StreamWriter sw = File.AppendText(GameOptions.datoteka))
                {
                    lik.Ime = _imeigraca;
                    sw.WriteLine(lik.Ime + " " + lik.Bodovi);
                }
                START = false;
                Wait(0.1);
                allSprites.Clear();
                GC.Collect();
                //glazba.Stop();
                Bodovi = lik.Bodovi;
                Kraj = true;
                this.Invoke((Action)delegate { this.Close(); });

            }

        }

        private void Provjera()
        {
            if (dobar.X ==los1.X || dobar.X==los2.X || dobar.TouchingSprite(los1)|| dobar.TouchingSprite(los2))
            {
                dobar.SetX(rn.Next(0, GameOptions.RightEdge - dobar.Width));
            }
            if (los1.X==los2.X || los1.TouchingSprite(los2))
            {
                los1.SetX(rn.Next(0, GameOptions.RightEdge - los1.Width));
            }
        }
        
        /* ------------ GAME CODE END ------------ */


    }
}
