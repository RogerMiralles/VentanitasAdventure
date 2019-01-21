using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureXam.Rooms
{
    class RoomClosed : Room
    {
        bool isClosed = true;
        public RoomClosed(int x, int y) : base(x, y)
        {
            int level = Program.ObteJuego().GetLevel();
            Player pl = Program.ObteJuego().pl;
            double random = CustomMath.RandomUnit();
            if (CustomMath.RandomUnit() < 1/3d)
            {
                ene = new Enemigo(Enemigo.eneList[CustomMath.RandomIntNumber(Enemigo.eneList.Length - 1)], ((int)Math.Pow(level + 1, 1.2) < pl.GetLevel()) ? pl.GetLevel() : (int) Math.Pow(level + 1, 1.4));
                ene.SetName("Super " + ene.GetName());
                if(random < 0.5)
                {
                    item[0] = new ItemWeapon("Espada legendaria", CustomMath.RandomIntNumber(5, 3) + level, 50);
                }
                else
                {
                    item[0] = new ItemWeapon("Espada de espadas", CustomMath.RandomIntNumber(3, 0) + level*2, 5);
                }
            }
            else
            {
                if (random < 0.05)
                {
                    item[0] = new ItemWeapon("Espada legendaria", CustomMath.RandomIntNumber(5, 3) + level, 50);
                }
                else if (random < 0.3)
                {
                    item[0] = new ItemWeapon("Espada buena", CustomMath.RandomIntNumber(3, 0) + level, 20);
                }
                else if (random < 0.95)
                {
                    item[0] = new ItemWeapon("Espada normal", (CustomMath.RandomIntNumber(3, 0) + level) / 2, 10);
                }
                else
                {
                    item[0] = new ItemWeapon("Espada podrida", 1, 0);
                }
            }
        }

        public bool IsClosed()
        {
            return isClosed;
        }

        public void OpenRoom()
        {
            isClosed = false;
        }

        public static bool UseKey()
        {
            Player pl = Program.ObteJuego().pl;
            ConsoleBuffer.ObteBuffer().InsertText("La habitación esta cerrada con llave");
            ConsoleBuffer.ObteBuffer().InsertText("¿Que objeto quieres usar para abrir la habitación?");
            pl.ListOfItems();
            ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height-3);
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2,">");
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
            Item[] bag = pl.GetBag();
            bool obj = int.TryParse(Console.ReadLine(), out int num);
            if (obj && pl.GetBag().Length > num && num >= 0 && bag[num] != null)
            {
                if (bag[num].GetName().Equals("Llave vieja"))
                {
                    ConsoleBuffer.ObteBuffer().InsertText("Has usado "+bag[num].GetName());
                    bag[num] = null;
                    Item.Ordenar(bag);
                    return true;
                }
                else
                {
                    ConsoleBuffer.ObteBuffer().InsertText("No parece que "+bag[num].GetName()+" encaje");
                }
            }
            else if(obj)
            {
                ConsoleBuffer.ObteBuffer().InsertText("Ese numero no es válido");
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("Tiene que ser un numero");
            }
            return false;
        }
    }
}
