using System;
using System.Collections.Generic;
using System.Threading;
using TextAdventureXam.Comandos;
using TextAdventureXam.Rooms;

namespace TextAdventureXam
{
    class Program
    {
         public string comando; 
         readonly int width = 120;
         readonly int height = 20;
         
         public Player pl;
         public List<Room> lvlLayout = null;
         public bool goNextLevel = false;
         public int level;

        private static Program juego;
        public static void CreaJuego()
        {
            if (juego == null)
            {
                juego = new Program();
            }
        }

        public static Program ObteJuego()
        {
            return juego;
        }



        public  void Antonio()
        {
            //Console.SetWindowSize(121, 22);
            ConsoleBuffer.CreaBuffer(width, height, height - 4,this);
            ConsoleKeyInfo k;
            do
            {
                pl = new Player();
                level = 1;
                ConsoleBuffer.ObteBuffer().ClearBox();
                int cRooms = 10 + 5 * level;
                Level.StartLevel(cRooms);
                lvlLayout = Level.GetListOfRooms();

                pl.currentRoom = lvlLayout[0];
                Introduccion();
                MainScreen();
                if (pl.IsDead())
                    ConsoleBuffer.ObteBuffer().Print(43, 10, "--HAS MUERTO--");
                else
                    ConsoleBuffer.ObteBuffer().Print(41, 10, "--TE HAS RENDIDO--");

                ConsoleBuffer.ObteBuffer().Print(39, 11, "pulsa r para reiniciar");
                ConsoleBuffer.ObteBuffer().PrintScreen();
                k = Console.ReadKey();
            } while (k.Key == ConsoleKey.R);
        }

        public  void Introduccion()
        {
            ConsoleBuffer.ObteBuffer().InsertText("En lo mas profundo de un bosque, en el pie de la montaña mas alta, se dice que existe la cueva del diablo, un lugar laberintico y lleno de monstruos. Se dice que cuando alguien entra no vuelve a salir nunca, o almenos no como humano.");
            ConsoleBuffer.ObteBuffer().InsertText("Tras años de viaje, un explorador se refugia dentro de una cueva. Al entrar, decide mirar el exterior, pero ya no encuentra salida");
            ConsoleBuffer.ObteBuffer().InsertText("Ese explorador eres tu");
            ConsoleBuffer.ObteBuffer().InsertText("");
            ConsoleBuffer.ObteBuffer().InsertText("Pulsa cualquier tecla para continuar");
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().PrintText(height - 3);
            ConsoleBuffer.ObteBuffer().PrintScreen();
            Console.ReadKey();
            ConsoleBuffer.ObteBuffer().ClearBox();
        }

        

        

        //Usado para guardar metodos dentro
        public delegate bool CommandMethod();


        //pantalla principal, donde pasa toda la magia
        public  void MainScreen()
        {
            CommandMethod method;
            string textInput;
            //Dibuja el fondo de primeras
            ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
            ConsoleBuffer.ObteBuffer().PrintBackground();
            ConsoleBuffer.ObteBuffer().InsertText(pl.currentRoom.GetDescriptionTotal());
            ConsoleBuffer.ObteBuffer().PrintText(height - 3);
            ConsoleBuffer.ObteBuffer().SmallMap();
            ConsoleBuffer.ObteBuffer().PrintScreen();

            do
            {
                //Pon el cursos en la posicion de escribir
                Console.SetCursorPosition(1, 18);

                Monitor.Enter(this);
                Monitor.Wait(this);

                //Espera input
                textInput = comando;

                //Espacio para facilitar lectura
                ConsoleBuffer.ObteBuffer().InsertText("");

                //Lo inserta en el lineTexto              
                ConsoleBuffer.ObteBuffer().InsertText(textInput);

                //Si el comando es valido
                if (Comando.CheckCommand(textInput))
                {
                    // obten el metodo
                    method = Comando.ReturnCommand(textInput);

                    //si ha obtenido metodo
                    if (method != null)
                    {
                        bool temp = method();
                        if ((method.Method.Name.Equals("MoveNorth") || method.Method.Name.Equals("MoveSouth") || method.Method.Name.Equals("MoveWest") || method.Method.Name.Equals("MoveEast")) && temp)
                        {
                            pl.currentRoom.SetVisible(2);
                            if (pl.GetMaldicion(1))
                            {
                                if (pl.GetHealth() > 1 && CustomMath.RandomUnit() < 0.1f)
                                {
                                    ConsoleBuffer.ObteBuffer().InsertText("Te has tropezado y has perdido 1 punto de vida");
                                    pl.ReceiveDamage(1, 0);
                                }
                            }

                            /*if (pl.currentRoom.ene != null)
                            {
                                ConsoleBuffer.ObteBuffer().InsertText("Un enemigo ha aparecido");
                                ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
                                ConsoleBuffer.ObteBuffer().PrintBackground();
                                ConsoleBuffer.ObteBuffer().PrintText(17);
                                SmallMap(this);
                                ConsoleBuffer.ObteBuffer().PrintScreen();
                                Console.ReadKey();
                                ConsoleBuffer cbMain = ConsoleBuffer.ObteBuffer();
                                ConsoleBuffer.ObteBuffer() = new ConsoleBuffer(width, height, height - 5,this);
                                EscenaCombate.Combate(pl.currentRoom.ene);
                                ConsoleBuffer.ObteBuffer() = cbMain;
                                if (pl.IsDead())
                                {
                                    textInput = "exit";
                                }
                            }*/

                            ConsoleBuffer.ObteBuffer().InsertText(pl.currentRoom.GetDescriptionTotal());
                        }
                    }
                    else
                    {
                        //En caso contrario avisa
                        ConsoleBuffer.ObteBuffer().InsertText("Not implemented");
                    }
                }
                else
                {
                    ConsoleBuffer.ObteBuffer().InsertText("Comando no valido");
                }
                if (goNextLevel)
                {
                    level++;
                    goNextLevel = false;
                    // hola
                    int cRooms = 10 + 5 * level;
                    Level.StartLevel(cRooms);
                    lvlLayout = Level.actualRooms;
                    pl.currentRoom = lvlLayout[0];
                    ConsoleBuffer.ObteBuffer().ClearBox();
                    if (pl.GetMaldicion(3))
                    {
                        if(CustomMath.RandomUnit() < 0.5)
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("Tu invalidez ha hecho que ruedes escaleras abajo de manera muy sonora y cómica. Milagrosamente no has recibido ningún daño. La escalera por la que has rebotado se ha destruido");
                        }
                        else
                        {
                            ConsoleBuffer.ObteBuffer().InsertText("Tu invalidez ha hecho que tus piernas dejen de responder y tengas que bajar usando las manos. Después de media hora has conseguido bajar. Al tocar el suelo del siguiente piso tus piernas vuelven a responder. La escalera por la que te has arrastrado se ha destruido");
                        }
                    }
                    else
                    {

                        ConsoleBuffer.ObteBuffer().InsertText("Cuando bajas escuchas un fuerte estruendo. Al mirar arriba las escaleras acaban en el techo de la sala");
                    }

                    ConsoleBuffer.ObteBuffer().InsertText(pl.currentRoom.GetDescriptionTotal());
                    if (pl.GetMaldicion(4))
                    {
                        ConsoleBuffer.ObteBuffer().InsertText("Sientes como un peso se levanta de ti");
                        for (int i = 0; i < pl.GetArrMal().Length; i++)
                        {
                            if (pl.GetArrMal()[i].GetId() == 4)
                            {
                                pl.GetArrMal()[i] = null;
                                i = pl.GetArrMal().Length;
                            }
                        }
                    }
                    
                }

                ConsoleBuffer.ObteBuffer().Print(1, 0, "PRINCIPAL");
                ConsoleBuffer.ObteBuffer().PrintBackground();
                ConsoleBuffer.ObteBuffer().PrintText(17);
                ConsoleBuffer.ObteBuffer().SmallMap();
                ConsoleBuffer.ObteBuffer().PrintScreen();
            } while (!textInput.Equals("exit") && !pl.IsDead());
        }

        public  int GetLevel()
        {
            return level;
        }
    }
}
