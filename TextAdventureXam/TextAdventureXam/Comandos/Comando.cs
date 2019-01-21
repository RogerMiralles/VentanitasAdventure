using System;
using System.Collections.Generic;
using TextAdventureXam.Rooms;

namespace TextAdventureXam.Comandos
{
    static class Comando
    {
        private static readonly string[] validCommands = { "oeste", "este", "norte", "sur", "map", "exit", "help", "stats", "sala", "coger", "soltar", "clear", "bajar", "mochila", "consumir", "equipo", "equipar", "desequipar", "rezar" };
        
        

        public static bool CheckCommand(string com)
        {
            foreach (string s in validCommands)
            {
                if (s.Equals(com.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static Program.CommandMethod ReturnCommand(string command)
        {
            Program.CommandMethod dd = null;
            switch (command.ToLower())
            {
                case "oeste":
                    dd = MoveWest;
                    if (Program.ObteJuego().pl.GetMaldicion(2) && CustomMath.RandomUnit() <0.05)
                        dd = MoveEast;
                    break;
                case "este":
                    dd = MoveEast;
                    if (Program.ObteJuego().pl.GetMaldicion(2) && CustomMath.RandomUnit() < 0.05)
                        dd = MoveWest;
                    break;
                case "norte":
                    dd =  MoveNorth;
                    if (Program.ObteJuego().pl.GetMaldicion(2) && CustomMath.RandomUnit() < 0.05)
                        dd = MoveSouth;
                    break;
                case "sur":
                    dd =  MoveSouth;
                    if (Program.ObteJuego().pl.GetMaldicion(2) && CustomMath.RandomUnit() < 0.05)
                        dd = MoveNorth;
                    break;
                case "map":
                    dd = DrawMap;
                    break;
                case "help":
                    dd = GetHelp;
                    break;
                case "stats":
                    dd = GetStats;
                    break;
                case "clear":
                    dd = ClearConsole;
                    break;
                case "sala":
                    dd = GetRoomDescr;
                    break;
                case "coger":
                    dd = PickItemFromRoom;
                    break;
                case "soltar":
                    dd = PlayerDropItem;
                    break;
                case "bajar":
                    dd = NextLevel;
                    break;
                case "mochila":
                    dd = LookAtBag;
                    break;
                case "consumir":
                    dd = ConsumeItem;
                    break;
                case "equipo":
                    dd = VerEquipo;
                    break;
                case "equipar":
                    dd = EquiparItem;
                    break;
                case "rezar":
                    dd = Rezar;
                    break;
                case "desequipar":
                    dd = Desequipar;
                    break;
            }
            return dd;
        }

        public static bool GetHelp()
        {
            ConsoleBuffer.ObteBuffer().InsertText("Lista de comandos:");
            ConsoleBuffer.ObteBuffer().InsertText("'help'           'mochila'");
            ConsoleBuffer.ObteBuffer().InsertText("'oeste'          'este'");
            ConsoleBuffer.ObteBuffer().InsertText("'norte'          'sur'");
            ConsoleBuffer.ObteBuffer().InsertText("'map'            'stats'");
            ConsoleBuffer.ObteBuffer().InsertText("'clear'          'sala'");
            ConsoleBuffer.ObteBuffer().InsertText("'coger'          'soltar'");
            ConsoleBuffer.ObteBuffer().InsertText("'equipar'        'equipo'");
            ConsoleBuffer.ObteBuffer().InsertText("'desequipar'     'consumir'");
            return true;
        }

        public static bool MoveNorth()
        {
            Room temp = Program.ObteJuego().pl.currentRoom.GetNorthRoom();
            if (temp != null)
            {
                if (temp.GetType() == typeof(RoomClosed) && ((RoomClosed)temp).IsClosed())
                {
                    if (RoomClosed.UseKey())
                    {
                        ((RoomClosed)temp).OpenRoom();
                        if (Program.ObteJuego().pl.GetMaldicion(4))
                            Program.ObteJuego().pl.currentRoom.SetVisible(0);
                        Program.ObteJuego().pl.currentRoom = temp;
                        ConsoleBuffer.ObteBuffer().InsertText("Vas al norte");
                        Program.ObteJuego().pl.lastRoom = 2;
                        return true;
                    }
                    else
                    {
                        if(!Program.ObteJuego().pl.GetMaldicion(4))
                            temp.SetVisible(1);
                        return false;
                    }
                }
                else
                {
                    if (Program.ObteJuego().pl.GetMaldicion(4))
                        Program.ObteJuego().pl.currentRoom.SetVisible(0);
                    Program.ObteJuego().pl.currentRoom = temp;
                    ConsoleBuffer.ObteBuffer().InsertText("Vas al norte");
                    Program.ObteJuego().pl.lastRoom = 2;
                    return true;
                }
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("No hay habitación al norte");
                return false;
            }
        }

        public static bool MoveWest()
        {
            Room temp = Program.ObteJuego().pl.currentRoom.GetWestRoom();
            if (temp != null)
            {
                if (temp.GetType() == typeof(RoomClosed) && ((RoomClosed)temp).IsClosed())
                {
                    if (RoomClosed.UseKey())
                    {
                        ((RoomClosed)temp).OpenRoom();
                        if (Program.ObteJuego().pl.GetMaldicion(4))
                            Program.ObteJuego().pl.currentRoom.SetVisible(0);
                        Program.ObteJuego().pl.currentRoom = temp;
                        ConsoleBuffer.ObteBuffer().InsertText("Vas al oeste");
                        Program.ObteJuego().pl.lastRoom = 3;
                        return true;
                    }
                    else
                    {
                        if (!Program.ObteJuego().pl.GetMaldicion(4))
                            temp.SetVisible(1);
                        return false;
                    }
                }
                else
                {
                    if (Program.ObteJuego().pl.GetMaldicion(4))
                        Program.ObteJuego().pl.currentRoom.SetVisible(0);
                    Program.ObteJuego().pl.currentRoom = temp;
                    ConsoleBuffer.ObteBuffer().InsertText("Vas al oeste");
                    Program.ObteJuego().pl.lastRoom = 3;
                    return true;
                }
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("No hay habitación al oeste");
                return false;
            }
        }

        public static bool MoveSouth()
        {
            Room temp = Program.ObteJuego().pl.currentRoom.GetSouthRoom();
            if (temp != null)
            {
                if (temp.GetType() == typeof(RoomClosed) && ((RoomClosed)temp).IsClosed())
                {
                    if (RoomClosed.UseKey())
                    {
                        ((RoomClosed)temp).OpenRoom();
                        if (Program.ObteJuego().pl.GetMaldicion(4))
                            Program.ObteJuego().pl.currentRoom.SetVisible(0);
                        Program.ObteJuego().pl.currentRoom = temp;
                        ConsoleBuffer.ObteBuffer().InsertText("Vas al sur");
                        Program.ObteJuego().pl.lastRoom = 0;
                        return true;
                    }
                    else
                    {
                        if (!Program.ObteJuego().pl.GetMaldicion(4))
                            temp.SetVisible(1);
                        return false;
                    }
                }
                else
                {
                    if (Program.ObteJuego().pl.GetMaldicion(4))
                        Program.ObteJuego().pl.currentRoom.SetVisible(0);
                    Program.ObteJuego().pl.currentRoom = temp;
                    ConsoleBuffer.ObteBuffer().InsertText("Vas al sur");
                    Program.ObteJuego().pl.lastRoom = 0;
                    return true;
                }
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("No hay habitación al sur");
                return false;
            }
        }

        public static bool MoveEast()
        {
            Room temp = Program.ObteJuego().pl.currentRoom.GetEastRoom();
            if (temp != null)
            {
                if(temp.GetType() == typeof(RoomClosed) && ((RoomClosed)temp).IsClosed())
                {
                    if (RoomClosed.UseKey())
                    {
                        ((RoomClosed)temp).OpenRoom();
                        if (Program.ObteJuego().pl.GetMaldicion(4))
                            Program.ObteJuego().pl.currentRoom.SetVisible(0);
                        Program.ObteJuego().pl.currentRoom = temp;
                        ConsoleBuffer.ObteBuffer().InsertText("Vas al este");
                        Program.ObteJuego().pl.lastRoom = 1;
                        return true;
                    }
                    else
                    {
                        if (!Program.ObteJuego().pl.GetMaldicion(4))
                            temp.SetVisible(1);
                        return false;
                    }
                }
                else
                {
                    if (Program.ObteJuego().pl.GetMaldicion(4))
                        Program.ObteJuego().pl.currentRoom.SetVisible(0);
                    Program.ObteJuego().pl.currentRoom = temp;
                    ConsoleBuffer.ObteBuffer().InsertText("Vas al este");
                    Program.ObteJuego().pl.lastRoom = 1;
                    return true;
                }
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("No hay habitación al este");
                return false;
            }
        }

        public static bool GetStats()
        {
            ConsoleBuffer.ObteBuffer().PrintBackground();
            Item[] bagitem = Program.ObteJuego().pl.GetGemas();
            int modh = 0;
            int moda = 0;
            int modd = 0;

            for (int i = 0; i < bagitem.Length; i++)
            {
                if(bagitem[i] != null && bagitem[i].GetType() == typeof(ItemGema))
                {
                    ItemGema gema = (ItemGema)bagitem[i];
                    modh += gema.ModifierHp();
                    moda += gema.ModifierAtt();
                    modd += gema.ModifierDef();
                }
            }
            ItemWeapon weapon = Program.ObteJuego().pl.GetWeapon();
            if (weapon != null)
            {
                modh += weapon.ModifierHp();
                moda += weapon.ModifierAtt();
                modd += weapon.ModifierDef();
            }
            ItemArmor armor = Program.ObteJuego().pl.GetArmor();
            if (armor != null)
            {
                modh += armor.ModifierHp();
                moda += armor.ModifierAtt();
                modd += armor.ModifierDef();
            }
            ConsoleBuffer.ObteBuffer().Print(1, 0, "STATS");

            ConsoleBuffer.ObteBuffer().Print(2, 3, "Nvl. " + Program.ObteJuego().pl.GetLevel()+"  Exp. "+Program.ObteJuego().pl.Experiencia);

            if(modh == 0)
                ConsoleBuffer.ObteBuffer().Print(2, 5, "VIDA (HP)-> " + Program.ObteJuego().pl.GetHealth() + "/" + Program.ObteJuego().pl.GetMHealth() + " --> Capacidad de aguante");
            else
                ConsoleBuffer.ObteBuffer().Print(2, 5, "VIDA (HP)-> " + Program.ObteJuego().pl.GetHealth() + "/" + Program.ObteJuego().pl.GetMHealth() + "+(" + modh+ ") --> Capacidad de aguante");

            if (moda == 0)
                ConsoleBuffer.ObteBuffer().Print(2, 7, "ATAQUE (Att)-> " + Program.ObteJuego().pl.GetFlatAtt() + " --> Daño que inflinges");
            else
                ConsoleBuffer.ObteBuffer().Print(2, 7, "ATAQUE (Att)-> " + Program.ObteJuego().pl.GetFlatAtt() + "+(" + moda + ") --> Daño que inflinges");

            if(modd == 0)
                ConsoleBuffer.ObteBuffer().Print(2, 9, "DEFENSA (Def)-> " + Program.ObteJuego().pl.GetFlatDef() + " --> Daño que reduces");
            else
                ConsoleBuffer.ObteBuffer().Print(2, 9, "DEFENSA (Def)-> " + Program.ObteJuego().pl.GetFlatDef() + "+(" + modd + ") --> Daño que reduces");

            ConsoleBuffer.ObteBuffer().Print(2, 13, "MANA (mana) -> " + Program.ObteJuego().pl.GetMana() + "/" + Program.ObteJuego().pl.GetManaM());

            ConsoleBuffer.ObteBuffer().Print(2, 15, "Velocidad (Vel.) -> " + Program.ObteJuego().pl.GetSpeed());

            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.ReadKey();
            return true;
        }

        public static bool ClearConsole() {
            ConsoleBuffer.ObteBuffer().ClearBox();
            Console.Clear();
            ConsoleBuffer.ObteBuffer().InsertText(Program.ObteJuego().pl.currentRoom.GetDescriptionTotal());
            return true;
        }

        public static bool GetRoomDescr()
        {
            ConsoleBuffer.ObteBuffer().InsertText(Program.ObteJuego().pl.currentRoom.GetDescriptionTotal());
            return true;
        }

        public static bool LookAtBag()
        {
            Item[] bag = Program.ObteJuego().pl.GetBag();
            for(int i = 0; i<bag.Length; i++)
            {
                int ii = i;
                int x = 0;
                if (ii >= 5)
                {
                    ii -= 5;
                    x = 1;
                }
                if(bag[i] != null)
                {
                    if (bag[i].GetType().BaseType == typeof(ItemEquipable))
                    {
                        ItemEquipable equipo = (ItemEquipable)bag[i];
                        ConsoleBuffer.ObteBuffer().Print(1 + 50*x, 2 + ii * 3, equipo.GetName());
                        string texto = "";
                        if (equipo.ModifierHp() < 0)
                            texto += "HP(" + equipo.ModifierHp() + ") ";
                        else
                            texto += "HP(+" + equipo.ModifierHp() + ") ";

                        if (equipo.ModifierAtt() < 0)
                            texto += "ATT(" + equipo.ModifierAtt() + ") ";
                        else
                            texto += "ATT(+" + equipo.ModifierAtt() + ") ";

                        if (equipo.ModifierDef() < 0)
                            texto += "DEF(" + equipo.ModifierDef() + ") ";
                        else
                            texto += "DEF(+" + equipo.ModifierDef() + ") ";

                        ConsoleBuffer.ObteBuffer().Print(5 + 50 * x, 3 + ii * 3, texto);
                    }
                    else if(bag[i].GetType() == typeof(ItemPocion))
                    {
                        ItemPocion consumable = (ItemPocion)bag[i];
                        ConsoleBuffer.ObteBuffer().Print(1+50*x, 2 + ii * 3, consumable.GetName());
                        if(consumable.GetPocionType() == ItemPocion.PocionType.hp)
                            ConsoleBuffer.ObteBuffer().Print(1+50*x, 3 + ii * 3, "    +"+consumable.GetFlatCant().ToString()+"% HP");
                        else
                            ConsoleBuffer.ObteBuffer().Print(1 + 50 * x, 3 + ii * 3, "    +" + consumable.GetFlatCant().ToString() + "% Mana");


                    }
                    else
                    {
                        ConsoleBuffer.ObteBuffer().Print(1 + 50 * x, 2 + ii * 3, bag[i].GetName());
                    }
                }
            }
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, "Pulsa cualquier boton para salir");
            ConsoleBuffer.ObteBuffer().Print(1, 0, "MOCHILA");
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.ReadKey();
            return true;
        }

        public static bool PickItemFromRoom()
        {
            Player pl = Program.ObteJuego().pl;
            if (pl.currentRoom.RoomHasItem())
            {
                ConsoleBuffer.ObteBuffer().InsertText("¿Que objeto quieres coger?");
                Item[] tempRoomItems = pl.currentRoom.GetRoomItems();
                pl.currentRoom.ListOfItems();
                ConsoleBuffer.ObteBuffer().PrintBackground();
                ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
                ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
                ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, ">");
                ConsoleBuffer.ObteBuffer().SmallMap();
                ConsoleBuffer.ObteBuffer().PrintScreen();
                Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
                bool obj = int.TryParse(Console.ReadLine(), out int num);

                if (!obj)
                {
                    ConsoleBuffer.ObteBuffer().InsertText("Solo acepta numeros");
                    return false;
                }

                if (num >= 0 && num <= tempRoomItems.Length - 1 && tempRoomItems[num] != null)
                {
                    pl.PickItem(num);
                    Item.Ordenar(tempRoomItems);
                    return true;
                }
                else
                {
                    ConsoleBuffer.ObteBuffer().InsertText("Ese número no es válido");
                }
            }
            ConsoleBuffer.ObteBuffer().InsertText("No hay nada que coger");
            return false;
        }
        
        public static bool PlayerDropItem()
        {
            //Check
            Item[] bag = Program.ObteJuego().pl.GetBag();
            bool empty = true;
            for(int i = 0; i < bag.Length; i++)
            {
                if(bag[i] != null)
                {
                    empty = false;
                }
            }
            if (empty)
            {
                ConsoleBuffer.ObteBuffer().InsertText("No tienes objetos equipados");
                return false;
            }
            ConsoleBuffer.ObteBuffer().InsertText("Que objeto quieres soltar?");
            Program.ObteJuego().pl.ListOfItems();
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
            ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, ">");
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
            int num;
            bool obj = int.TryParse(Console.ReadLine(), out num);
            if (obj && num >= 0 && num < bag.Length && bag[num] != null)
            {
                Program.ObteJuego().pl.DropItem(num);
                Item.Ordenar(Program.ObteJuego().pl.GetBag());
                return true;
            }
            else if(!obj)
            {
                ConsoleBuffer.ObteBuffer().InsertText("Tiene que ser un numero");
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("Esa posicion no es válida");
            }
            return false;
        }

        public static bool NextLevel()
        {
            Room c = Program.ObteJuego().pl.currentRoom;
            if (c.GetType() == typeof(RoomExit))
            {
                Program.ObteJuego().goNextLevel = true;
                return true;
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("No hay ninguna escalera por bajar");
                return false;
            }
        }

        public static bool ConsumeItem()
        {
            Item[] bag = Program.ObteJuego().pl.GetBag();
            ConsoleBuffer.ObteBuffer().InsertText("Que objeto quieres consumir?");
            Program.ObteJuego().pl.ListOfItems();
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
            ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, ">");
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
            int num;
            bool obj = int.TryParse(Console.ReadLine(), out num);
            if (obj && num >= 0 && num < bag.Length && bag[num] != null)
            {
                return Program.ObteJuego().pl.ConsumeItem(num);
            }
            else if (!obj)
            {
                ConsoleBuffer.ObteBuffer().InsertText("Tiene que ser un numero");
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("Esa posicion no es válida");
            }
            return false;
        }

        public static bool VerEquipo()
        {
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().Print(1, 0, "EQUIPO");
            ConsoleBuffer.ObteBuffer().Print(1, 4, "ARMA");
            ItemWeapon weapon = Program.ObteJuego().pl.GetWeapon();
            if (weapon != null)
            {
                ConsoleBuffer.ObteBuffer().Print(7, 5, weapon.GetName());
                ConsoleBuffer.ObteBuffer().Print(7, 6, "HP-> " + weapon.ModifierHp());
                ConsoleBuffer.ObteBuffer().Print(7, 7, "ATT-> " + weapon.ModifierAtt());
                ConsoleBuffer.ObteBuffer().Print(7, 8, "DEF-> " + weapon.ModifierDef());
            }
            else
            {
                ConsoleBuffer.ObteBuffer().Print(7, 5, "Ninguna");
            }


            ConsoleBuffer.ObteBuffer().Print(1, 12, "ARMADURA");
            ItemArmor armor = Program.ObteJuego().pl.GetArmor();
            if (armor != null)
            {
                ConsoleBuffer.ObteBuffer().Print(7, 13, armor.GetName());
                ConsoleBuffer.ObteBuffer().Print(7, 14, "HP-> " + armor.ModifierHp());
                ConsoleBuffer.ObteBuffer().Print(7, 15, "ATT-> " + armor.ModifierAtt());
                ConsoleBuffer.ObteBuffer().Print(7, 16, "DEF-> " + armor.ModifierDef());
            }
            else
            {
                ConsoleBuffer.ObteBuffer().Print(7, 5, "Ninguna");
            }

            ConsoleBuffer.ObteBuffer().Print(101, 3, "Hp  -> " + Program.ObteJuego().pl.GetHealth() + "/" + Program.ObteJuego().pl.GetMHealth());
            ConsoleBuffer.ObteBuffer().Print(101, 5, "Att -> " + Program.ObteJuego().pl.GetAtt());
            ConsoleBuffer.ObteBuffer().Print(101, 7, "Def -> " + Program.ObteJuego().pl.GetDef());
            ConsoleBuffer.ObteBuffer().Print(101, 11, "Mana -> " + Program.ObteJuego().pl.GetMana() + "/" + Program.ObteJuego().pl.GetManaM());
            ConsoleBuffer.ObteBuffer().Print(101, 13, "Vel. -> " + Program.ObteJuego().pl.GetSpeed());

            ConsoleBuffer.ObteBuffer().Print(51, 4, "GEMAS");

            ItemGema[] gemas = Program.ObteJuego().pl.GetGemas();
            bool check = true;
            for(int i = 0; i< gemas.Length; i++)
            {
                if (gemas[i] != null)
                {
                    check = false;
                    if (i < 2)
                    {
                        ConsoleBuffer.ObteBuffer().Print(55, 5 + i * 7, i.ToString()+":");
                        ConsoleBuffer.ObteBuffer().Print(57, 6 + i * 7, gemas[i].GetName());
                        ConsoleBuffer.ObteBuffer().Print(57, 7 + i * 7, "HP-> " + gemas[i].ModifierHp());
                        ConsoleBuffer.ObteBuffer().Print(57, 8 + i * 7, "ATT-> " + gemas[i].ModifierAtt());
                        ConsoleBuffer.ObteBuffer().Print(57, 9 + i * 7, "DEF-> " + gemas[i].ModifierDef());
                    }
                    else
                    {
                        ConsoleBuffer.ObteBuffer().Print(78, 5, i.ToString() + ":");
                        ConsoleBuffer.ObteBuffer().Print(80, 6, gemas[i].GetName());
                        ConsoleBuffer.ObteBuffer().Print(80, 7, "HP-> " + gemas[i].ModifierHp());
                        ConsoleBuffer.ObteBuffer().Print(80, 8, "ATT-> " + gemas[i].ModifierAtt());
                        ConsoleBuffer.ObteBuffer().Print(80, 9, "DEF-> " + gemas[i].ModifierDef());
                    }
                }
            }

            if (check)
            {
                ConsoleBuffer.ObteBuffer().Print(57, 5, "No tienes gemas");
            }

            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, "Pulsa cualquier tecla para salir");
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.ReadKey();
            return true;
        }

        public static bool EquiparItem()
        {
            Item[] bag = Program.ObteJuego().pl.GetBag();
            ConsoleBuffer.ObteBuffer().InsertText("¿Que objeto quieres equiparte?");
            Program.ObteJuego().pl.ListOfItems();
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
            ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
            ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, ">");
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
            int num;
            bool obj = int.TryParse(Console.ReadLine(), out num);
            if (obj && num >= 0 && num < bag.Length && bag[num] != null)
            {
                Program.ObteJuego().pl.EquipItem(num);
                return true;
            }
            else if (!obj)
            {
                ConsoleBuffer.ObteBuffer().InsertText("Tiene que ser un numero");
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("Esa posicion no es válida");
            }
            return true;
        }
        public static bool Desequipar()
        {
            if (Program.ObteJuego().pl.FilledBag())
            {
                ConsoleBuffer.ObteBuffer().InsertText("Tienes la mochila llena");
                return false;
            }
            else
            {
                ConsoleBuffer.ObteBuffer().InsertText("¿Que quieres desequiparte?");
                ConsoleBuffer.ObteBuffer().InsertText("    >ARMA    >GEMA    >ARMADURA");
                ConsoleBuffer.ObteBuffer().Print(1,ConsoleBuffer.ObteBuffer().height-2,">");
                ConsoleBuffer.ObteBuffer().PrintBackground();
                ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
                ConsoleBuffer.ObteBuffer().SmallMap();
                ConsoleBuffer.ObteBuffer().PrintScreen();
                Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
                string tipo = Console.ReadLine().ToLower();
                if (tipo.Equals("arma"))
                {
                    if (Program.ObteJuego().pl.GetWeapon() != null)
                    {
                        for (int i = 0; i < Program.ObteJuego().pl.GetBag().Length; i++)
                        {
                            if (Program.ObteJuego().pl.GetBag()[i] == null)
                            {
                                Item rr = Program.ObteJuego().pl.DropWeapon();
                                ConsoleBuffer.ObteBuffer().InsertText("Te has desequipado "+rr.GetName());
                                Program.ObteJuego().pl.GetBag()[i] = rr;
                                i = Program.ObteJuego().pl.GetBag().Length;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("No tienes arma equipada");
                        return false;
                    }
                }
                else if(tipo.Equals("gema"))
                {
                    if (Program.ObteJuego().pl.EmptyGemas())
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("No tienes gemas equipadas");
                        return false;
                    }
                    else
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("¿Que gema quieres desequiparte?");
                        Program.ObteJuego().pl.ListOfGems();
                        ConsoleBuffer.ObteBuffer().Print(1, ConsoleBuffer.ObteBuffer().height - 2, ">");
                        ConsoleBuffer.ObteBuffer().PrintBackground();
                        ConsoleBuffer.ObteBuffer().PrintText(ConsoleBuffer.ObteBuffer().height - 3);
                        ConsoleBuffer.ObteBuffer().PrintScreen();
                        Console.SetCursorPosition(2, ConsoleBuffer.ObteBuffer().height - 2);
                        bool obj = int.TryParse(Console.ReadLine().ToLower(),out int gema);
                        if (obj && gema >= 0 && gema < Program.ObteJuego().pl.GetGemas().Length && Program.ObteJuego().pl.GetGemas()[gema] != null)
                        {
                            ItemGema r = Program.ObteJuego().pl.GetGemas()[gema];
                            Program.ObteJuego().pl.GetGemas()[gema] = null;
                            for (int i = 0; i < Program.ObteJuego().pl.GetBag().Length; i++)
                            {
                                if (Program.ObteJuego().pl.GetBag()[i] == null)
                                {
                                    Program.ObteJuego().pl.GetBag()[i] = r;
                                    ConsoleBuffer.ObteBuffer().InsertText("Te has desequipado '"+r.GetName()+"'");
                                    i = Program.ObteJuego().pl.GetBag().Length;
                                }
                            }
                            return true;
                        }
                        else if(obj)
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("Tiene que ser un número");
                        }
                        else
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("El número no es válido");
                        }
                        return false;
                    }
                }
                else if (tipo.Equals("armadura"))
                {
                    if (Program.ObteJuego().pl.GetArmor() != null)
                    {
                        for (int i = 0; i < Program.ObteJuego().pl.GetBag().Length; i++)
                        {
                            if (Program.ObteJuego().pl.GetBag()[i] == null)
                            {
                                Item rr = Program.ObteJuego().pl.DropArmor();
                                ConsoleBuffer.ObteBuffer().InsertText("Te has desequipado " + rr.GetName());
                                Program.ObteJuego().pl.GetBag()[i] = rr;
                                i = Program.ObteJuego().pl.GetBag().Length;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("No tienes armadura equipada");
                        return false;
                    }
                }
                else
                {
                    ConsoleBuffer.ObteBuffer().InsertText("Comando no válido");
                    return false;
                }
            }
        }
        public static bool Rezar()
        {
            ConsoleBuffer.ObteBuffer().InsertText("De rodillas, empiezas a rezar");
            if (Program.ObteJuego().pl.currentRoom.GetType() == typeof(RoomBless))
            {
                RoomBless temp =(RoomBless) Program.ObteJuego().pl.currentRoom;
                if (temp.IsBlessed())
                {
                    temp.Effect();
                    return true;
                }
            }
            ConsoleBuffer.ObteBuffer().InsertText("No ha pasado nada");
            return false;
        }

        public static bool DrawMap()
        {
            Player pl = Program.ObteJuego().pl;
            List<Room> lvlLayout = Program.ObteJuego().lvlLayout;
            int miniMapx = pl.currentRoom.GetPosX() * 2;
            int miniMapy = pl.currentRoom.GetPosY() * 2;
            int width = ConsoleBuffer.ObteBuffer().width;
            int height = ConsoleBuffer.ObteBuffer().height;
            int level = Program.ObteJuego().level;

            ConsoleKeyInfo keyInfo;
            do
            {
                for (int i = 0; i < lvlLayout.Count; i++)
                {
                    if (lvlLayout[i].IsVisible() != 0)
                    {
                        int xx = (lvlLayout[i].GetPosX()) * 2 - miniMapx + (width - 20) / 2;
                        int yy = (-lvlLayout[i].GetPosY()) * 2 + miniMapy + height / 2;
                        if (xx >= 1 && xx < width - 21 && yy >= 2 && yy < height - 1)
                        {
                            if (lvlLayout[i].IsVisible() == 2)
                            {
                                if (pl.currentRoom.GetPosX() == lvlLayout[i].GetPosX() && pl.currentRoom.GetPosY() == lvlLayout[i].GetPosY())
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "O");
                                else if (lvlLayout[i].GetType() == typeof(RoomTreasure))
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "Z");
                                else if (lvlLayout[i].GetType() == typeof(RoomExit))
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "S");
                                else if (lvlLayout[i].GetType() == typeof(RoomClosed))
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "T");
                                else if (lvlLayout[i].GetType() == typeof(RoomBless))
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "B");
                                else
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "X");

                                if (lvlLayout[i].GetNorthRoom() != null)
                                {
                                    if (yy >= 3)
                                        ConsoleBuffer.ObteBuffer().Print(xx, yy - 1, "|");
                                }
                                if (lvlLayout[i].GetSouthRoom() != null)
                                {
                                    if (yy < height - 2)
                                        ConsoleBuffer.ObteBuffer().Print(xx, yy + 1, "|");
                                }
                                if (lvlLayout[i].GetWestRoom() != null)
                                {
                                    if (xx > 0)
                                        ConsoleBuffer.ObteBuffer().Print(xx - 1, yy, "-");
                                }
                                if (lvlLayout[i].GetEastRoom() != null)
                                {
                                    if (xx < width - 22)
                                        ConsoleBuffer.ObteBuffer().Print(xx + 1, yy, "-");
                                }
                            }
                            else
                            {
                                if (pl.currentRoom.GetPosX() == lvlLayout[i].GetPosX() && pl.currentRoom.GetPosY() == lvlLayout[i].GetPosY())
                                    throw new Exception("Sala invisible en propia localización");
                                else
                                    ConsoleBuffer.ObteBuffer().Print(xx, yy, "?");
                                if (lvlLayout[i].IsVisible() == 3)
                                {
                                    if (lvlLayout[i].GetNorthRoom() != null)
                                    {
                                        if (yy >= 3)
                                            ConsoleBuffer.ObteBuffer().Print(xx, yy - 1, "|");
                                    }
                                    if (lvlLayout[i].GetSouthRoom() != null)
                                    {
                                        if (yy < height - 2)
                                            ConsoleBuffer.ObteBuffer().Print(xx, yy + 1, "|");
                                    }
                                    if (lvlLayout[i].GetWestRoom() != null)
                                    {
                                        if (xx > width - 19)
                                            ConsoleBuffer.ObteBuffer().Print(xx - 1, yy, "-");
                                    }
                                    if (lvlLayout[i].GetEastRoom() != null)
                                    {
                                        if (xx < width - 2)
                                            ConsoleBuffer.ObteBuffer().Print(xx + 1, yy, "-");
                                    }
                                }
                            }
                        }
                    }
                }

                ConsoleBuffer.ObteBuffer().Print(1, 0, "MAP");
                ConsoleBuffer.ObteBuffer().Print(71, 0, "Usa flechas para mover mapa");
                ConsoleBuffer.ObteBuffer().Print(1, 2, "O -> Tu");
                ConsoleBuffer.ObteBuffer().Print(1, 3, "T, Z, B -> Especial");
                ConsoleBuffer.ObteBuffer().Print(1, 4, "S -> Salida");
                ConsoleBuffer.ObteBuffer().Print(101, 0, "Planta: " + -(level - 1));
                ConsoleBuffer.ObteBuffer().PrintBackground();

                ConsoleBuffer.ObteBuffer().PrintScreen();
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        miniMapy -= 2;
                        break;
                    case ConsoleKey.UpArrow:
                        miniMapy += 2;
                        break;
                    case ConsoleKey.LeftArrow:
                        miniMapx -= 2;
                        break;
                    case ConsoleKey.RightArrow:
                        miniMapx += 2;
                        break;
                }
            } while (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow);
            return true;
        }

    }
}
