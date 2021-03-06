﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureXam.Rooms
{
    class RoomBless : Room
    {
        bool hasEffect = true;
        public RoomBless(int x, int y) : base(x, y)
        {
            descr = "La estatua de un angel de pie esta en el centro de la habitación, puedes <<rezar>>";
        }

        public bool IsBlessed()
        {
            return hasEffect;
        }

        public void Effect()
        {

            if (hasEffect)
            {
                do
                {
                    int efecto = CustomMath.RandomIntNumber(3);
                    Player pl = Program.ObteJuego().pl;
                    if (efecto == 0)
                    {
                        List<int> r = new List<int>();
                        for (int i = 0; i < pl.GetArrMal().Length; i++)
                        {
                            if (pl.GetArrMal()[i] != null)
                            {
                                r.Add(i);
                            }
                        }
                        if (r.Count > 0)
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("Al rezar, sientes como tu cuerpo se siente mas ligero");
                            int temp = CustomMath.RandomIntNumber(r.Count - 1);
                            ConsoleBuffer.ObteBuffer().InsertText("¡La " + pl.GetArrMal()[r[temp]].GetName() + " ha desaparecido!");
                            pl.GetArrMal()[r[temp]] = null;
                            hasEffect = false;
                        }
                    }
                    else if(efecto == 1)
                    {
                        Item item;

                        if (CustomMath.RandomUnit() < 0.9)
                        {
                            if (CustomMath.RandomUnit() < 0.5)
                            {
                                item = new ItemPocion("Gran poción de Vida", 100, ItemPocion.PocionType.hp);
                            }
                            else
                            {
                                item = new ItemPocion("Gran poción de Maná", 100, ItemPocion.PocionType.mana);
                            }
                        }
                        else
                        {
                            double prob = CustomMath.RandomUnit();
                            if (prob < 0.5)
                            {
                                item = new ItemScroll("Pergamino de visión", 0);
                            }
                            else
                            {
                                item = new ItemScroll("Pergamino de salida", 1);
                            }
                        }

                        if (!pl.FilledBag())
                        {
                            pl.GetItem(item);
                            ConsoleBuffer.ObteBuffer().InsertText("Un objeto ha aparecido en tu mochila");
                            hasEffect = false;
                        }
                        else if (GetItem(item))
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("Ha aparecido un objeto en la sala");
                            hasEffect = false;
                        }
                    }
                    else if(efecto == 2)
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("Los ojos los tienes más despiertos y eres capaz de ver en la oscuridad");
                        hasEffect = false;
                        List<Room> r = Program.ObteJuego().lvlLayout;
                        for (int i = 0; i<r.Count; i++)
                        {
                            if(r[i].IsVisible() == 0)
                                r[i].SetVisible(3);
                        }
                        if (pl.GetMaldicion(4))
                        {
                            for(int i = 0; i< pl.GetArrMal().Length; i++)
                            {
                                if(pl.GetArrMal()[i].GetId() == 4)
                                {
                                    pl.GetArrMal()[i] = null;
                                    i = pl.GetArrMal().Length;
                                    ConsoleBuffer.ObteBuffer().InsertText("¡Has perdido la Maldición del ciego!");
                                }
                            }
                        }
                    }
                    else if(efecto == 3 && CustomMath.RandomUnit() < 0.5)
                    {
                        for (int i = 0; i < pl.GetArrMal().Length; i++)
                            pl.GetArrMal()[i] = null;

                        pl.ExcesoMaldito = 0;
                        pl.RestoreHealth();
                        pl.RestoreMana();
                        ConsoleBuffer.ObteBuffer().InsertText("¡Tu cuerpo se siente renacido!");
                        ConsoleBuffer.ObteBuffer().InsertText("¡Has recuperado toda la vida!");
                        ConsoleBuffer.ObteBuffer().InsertText("¡Has recuperado todo el maná!");
                        ConsoleBuffer.ObteBuffer().InsertText("¡Todas las maldiciones se han desvanecido!");
                    }
                } while (hasEffect);
            }
        }
    }
}
