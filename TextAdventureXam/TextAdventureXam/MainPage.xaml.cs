using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace TextAdventureXam
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Program.CreaJuego();
            Program juego = Program.ObteJuego();
            Thread thrad = new Thread(new ThreadStart(juego.Antonio));            
        }
        
        public void PalNorte(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "norte";
                Monitor.Exit(Program.ObteJuego());
            }
            
        }

    }
}
