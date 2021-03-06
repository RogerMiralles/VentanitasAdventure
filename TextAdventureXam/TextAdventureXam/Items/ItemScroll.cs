﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureXam.Rooms;

namespace TextAdventureXam
{
    class ItemScroll : ItemConsumable
    {
        readonly int id;
        public ItemScroll(string name, int id) : base(name)
        {
            this.id = id;
        }

        public override void Consumir()
        {
            Effect();
        }

        private void Effect()
        {
            Player pl = Program.ObteJuego().pl;
            ConsoleBuffer buffer = ConsoleBuffer.ObteBuffer();
            switch (id)
            {
                case 0:
                    if (!pl.GetMaldicion(4))
                    {
                        List<Room> r0 = Program.ObteJuego().lvlLayout;
                        for (int i = 0; i < r0.Count; i++)
                        {
                            if(r0[i].IsVisible() == 0)
                                r0[i].SetVisible(3);
                        }
                        buffer.InsertText("¡El piso se ha revelado!");
                    }
                    else
                    {
                        buffer.InsertText("¡La maldición del ciego ha anulado el efecto!");
                    }
                    break;

                case 1:
                    List<Room> r1 = Program.ObteJuego().lvlLayout;
                    for (int i = 0; i < r1.Count; i++)
                    {
                        if (r1[i].GetType() == typeof(RoomExit))
                        {
                            if (pl.GetMaldicion(4))
                                pl.currentRoom.SetVisible(0);
                            pl.currentRoom = r1[i];
                            r1[i].SetVisible(2);
                            buffer.InsertText("¡Te has teletransportado!");
                            buffer.InsertText(r1[i].GetDescriptionTotal());
                            i = r1.Count;
                        }
                    }
                    break;
            }
        }
    }
}
