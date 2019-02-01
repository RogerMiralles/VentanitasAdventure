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
            ConsoleBuffer.CreaBuffer(120, 19, 19 - 4, juego, texto1);
            Thread thrad = new Thread(new ThreadStart(juego.Antonio));
            thrad.Start();           
            
            //TextBox.Text = "hola";
            //Label e = (Label) TextAdventureXam.texto1;

        }       
        
        public void TestEscribir(object sender,EventArgs e)
        {
            texto1.Text = "111";
            
            
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

        public void PalSur(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "sur";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void PalEste(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "este";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void PalOeste(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "oeste";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoHelp(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "help";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoMap(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "map";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoClear(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "clear";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoCoger(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "coger";
                Monitor.Exit(Program.ObteJuego());
            }
        }

        public void ComandoEquipar(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "equipar";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoDesequipar(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "desequipar";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoMochila(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "mochila";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoStats(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "stats";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoSoltar(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "soltar";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoSala(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "sala";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoEquipo(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "equipo";
                Monitor.Exit(Program.ObteJuego());
            }

        }

        public void ComandoConsumir(object sender, EventArgs e)
        {
            bool tieneLock = Monitor.TryEnter(Program.ObteJuego());
            if (tieneLock)
            {
                Monitor.PulseAll(Program.ObteJuego());
                Program.ObteJuego().comando = "consumir";
                Monitor.Exit(Program.ObteJuego());
            }

        }

    }
}
