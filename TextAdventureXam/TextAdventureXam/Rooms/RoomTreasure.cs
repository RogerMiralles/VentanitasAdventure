using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureXam.Rooms
{
    class RoomTreasure : Room
    {
        bool isCurse;

        public RoomTreasure(int x, int y) : base(x, y)
        {
            int level = Program.ObteJuego().level;
            this.x = x;
            this.y = y;
            item[0] = new ItemGema("Gema", 0, CustomMath.RandomIntNumber(3+level,-1 + level), CustomMath.RandomIntNumber(3 + level, -1 + level));
            descr = "Al entrar, sientes una gran presencia. Dentro de la sala, en un pedestal, hay una gema con una tenue luz que ilumina a duras penas las paredes de la sala.";
            ene = null;
            isCurse = (CustomMath.RandomUnit() < 0.6) ? true : false;
        }

        override public Item DropItem(int i)
        {

            Item value = base.DropItem(i);
            if(value != null && i == 0)
            {
                descr = "Una sala con un pedestal en el centro";
                if (isCurse)
                {
                    isCurse = false;
                    ConsoleBuffer.ObteBuffer().InsertText("Al coger '"+value.GetName()+"' sientes como tu cuerpo se vuelve mas pesado");
                    Program.ObteJuego().pl.ObtenMaldicion();
                }
            }

            return value;
        }
    }
}
