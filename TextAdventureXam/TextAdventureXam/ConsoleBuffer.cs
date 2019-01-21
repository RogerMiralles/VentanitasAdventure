using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureXam
{
    class ConsoleBuffer
    {
        private string[] lines;
        public int width;
        public int height;
        private string[] textBox;
        private Program juego;
        private static ConsoleBuffer buffer; 

        public static void CreaBuffer(int width, int height, int sizeTextBox, Program x)
        {
            if (buffer == null)
            {
                buffer = new ConsoleBuffer(width,height,sizeTextBox,x);
            }
        }
        public static ConsoleBuffer ObteBuffer()
        {
            return buffer;
        }

        private ConsoleBuffer(int width, int height, int sizeTextBox,Program x)
        {
            this.juego = x;
            this.width = width;
            this.height = height;
            lines = new string[height];
            textBox = new string[sizeTextBox];
            string str = "";
            for (int i = 0; i<width; i++)
            {
                str += " ";
            }
            for(int i = 0; i<height; i++)
            {
                if(i < sizeTextBox)
                {
                    textBox[i] = "";
                }
                lines[i] = str;
            }
        }

        public void Print(int x, int y, string text)
        {
            string temp = "";

            for(int i = 0; i<lines[y].Length; i++)
            {
                if(i >= x && i < x + text.Length)
                {
                    temp += text[i - x];
                }
                else
                {
                    temp += lines[y][i];
                }

                if(i == width-1 && i-x < text.Length - 1)
                {
                    Print(0, y + 1, text.Substring(i+1 - x));
                }
            }
            lines[y] = temp;
        }

        public void PrintScreen()
        {
            //Console.SetCursorPosition(0, 0);
            for(int i = 0; i<lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }

            string str = "";
            for (int i = 0; i < width; i++)
            {
                str += " ";
            }
            for (int i = 0; i < height; i++)
            {
                lines[i] = str;
            }

            //Console.SetCursorPosition(0, height-2);
        }

        public void PrintText(int start)
        {
            for (int i = 0; i < textBox.Length; i++)
            {
                Print(1, start - i, textBox[i]);
            }
        }

        public void InsertText(string newText)
        {
            if (juego.pl.GetMaldicion(0))
            {
                string prohibido = "\"<>-|[]0987654321¿?!¡,.:'";
                string[] mald = newText.Split(' ');
                newText = "";
                for (int i = 0; i < mald.Length; i++)
                {
                    if (mald[i].Length >= 3)
                    {
                        int numeroCoin = 0;
                        for (int j = 0; j < mald[i].Length; j++)
                        {
                            for (int k = 0; k < prohibido.Length; k++)
                            {
                                if (prohibido[k] == mald[i][j])
                                {
                                    numeroCoin++;
                                    k = prohibido.Length;
                                }
                            }
                        }
                        if (numeroCoin != mald[i].Length)
                        {
                            int r1, r2;
                            bool check;
                            do
                            {
                                check = false;
                                r1 = CustomMath.RandomIntNumber(mald[i].Length - 1);
                                for (int j = 0; j < prohibido.Length; j++)
                                {
                                    if (mald[i][r1] == prohibido[j])
                                    {
                                        check = true;
                                    }
                                }
                            } while (check);
                            do
                            {
                                check = false;
                                r2 = CustomMath.RandomIntNumber(mald[i].Length - 1);
                                for (int j = 0; j < prohibido.Length; j++)
                                {
                                    if (mald[i][r2] == prohibido[j])
                                    {
                                        check = true;
                                    }
                                }
                            } while (check);

                            char temp = mald[i][r1];
                            if (r1 + 1 < mald[i].Length)
                                mald[i] = mald[i].Substring(0, r1) + mald[i][r2] + mald[i].Substring(r1 + 1);
                            else
                                mald[i] = mald[i].Substring(0, r1) + mald[i][r2];
                            if (r2 + 1 < mald[i].Length)
                                mald[i] = mald[i].Substring(0, r2) + temp + mald[i].Substring(r2 + 1);
                            else
                                mald[i] = mald[i].Substring(0, r2) + temp;
                        }
                    }
                    newText += mald[i];
                    if (i + 1 < mald.Length)
                        newText += " ";
                }
            }

            if (newText.Length > width - 22)
            {
                for (int i = textBox.Length - 1; i >= 1; i--)
                {
                    textBox[i] = textBox[i - 1];
                }

                int limit = newText.Length - 1;
                while (limit >= 0 && (limit > width - 22 || !newText[limit].Equals(' ')))
                {
                    limit--;
                }
                if (limit == -1 || limit == 0)
                {
                    textBox[0] = newText.Substring(limit+1, width - 22);
                    InsertText(newText.Substring(width - 22));
                }
                else
                {
                    textBox[0] = newText.Substring(0, limit);
                    InsertText(newText.Substring(limit));
                }
            }
            else
            {
                for (int i = textBox.Length - 1; i >= 1; i--)
                {
                    textBox[i] = textBox[i - 1];
                }

                textBox[0] = newText;
            }
        }

        public void InsertText(string[] newTextA)
        {
            for (int j = 0; j < newTextA.Length; j++)
            {
                string newText = newTextA[j];
                InsertText(newText);
            }
        }

        public void PrintBackground()
        {
            Print(0, 1, "-----------------------------------------------------------------------------------------------------------------------");
            Print(0, height - 1, "------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < height; i++)
            {
                Print(0, i, "|");
                Print(width - 21, i, "|");
                Print(width - 1, i, "|");
            }
        }

        public void ClearBox()
        {
            for(int i = 0; i<textBox.Length; i++)
            {
                textBox[i] = "";
            }
        }
        public void SmallMap()
        {
            Player pl = juego.pl;
            int miniMapx = pl.currentRoom.GetPosX() * 2;
            int miniMapy = pl.currentRoom.GetPosY() * 2;
            List<Rooms.Room> lvlLayout = juego.lvlLayout;
            
            for (int i = 0; i < lvlLayout.Count; i++)
            {
                if (lvlLayout[i].IsVisible() != 0)
                {
                    int xx = (lvlLayout[i].GetPosX()) * 2 - miniMapx + width - 10;
                    int yy = (-lvlLayout[i].GetPosY()) * 2 + miniMapy + height / 2;
                    if (xx >= width - 20 && xx < width - 1 && yy >= 2 && yy < height - 1)
                    {
                        if (lvlLayout[i].IsVisible() == 2)
                        {
                            if (pl.currentRoom.GetPosX() == lvlLayout[i].GetPosX() && pl.currentRoom.GetPosY() == lvlLayout[i].GetPosY())
                                ObteBuffer().Print(xx, yy, "O");
                            else if (lvlLayout[i].GetType() == typeof(Rooms.RoomTreasure))
                                ObteBuffer().Print(xx, yy, "Z");
                            else if (lvlLayout[i].GetType() == typeof(Rooms.RoomExit))
                                ObteBuffer().Print(xx, yy, "S");
                            else if (lvlLayout[i].GetType() == typeof(Rooms.RoomClosed))
                                ObteBuffer().Print(xx, yy, "T");
                            else if (lvlLayout[i].GetType() == typeof(Rooms.RoomBless))
                                ObteBuffer().Print(xx, yy, "B");
                            else
                                ObteBuffer().Print(xx, yy, "X");

                            if (lvlLayout[i].GetNorthRoom() != null)
                            {
                                if (yy >= 3)
                                    ObteBuffer().Print(xx, yy - 1, "|");
                            }
                            if (lvlLayout[i].GetSouthRoom() != null)
                            {
                                if (yy < height - 2)
                                    ObteBuffer().Print(xx, yy + 1, "|");
                            }
                            if (lvlLayout[i].GetWestRoom() != null)
                            {
                                if (xx > width - 19)
                                    ObteBuffer().Print(xx - 1, yy, "-");
                            }
                            if (lvlLayout[i].GetEastRoom() != null)
                            {
                                if (xx < width - 2)
                                    ObteBuffer().Print(xx + 1, yy, "-");
                            }
                        }
                        else
                        {
                            if (pl.currentRoom.GetPosX() == lvlLayout[i].GetPosX() && pl.currentRoom.GetPosY() == lvlLayout[i].GetPosY())
                                throw new Exception("Sala invisible en propia localización");
                            else
                                ObteBuffer().Print(xx, yy, "?");

                            if (lvlLayout[i].IsVisible() == 3)
                            {
                                if (lvlLayout[i].GetNorthRoom() != null)
                                {
                                    if (yy >= 3)
                                        ObteBuffer().Print(xx, yy - 1, "|");
                                }
                                if (lvlLayout[i].GetSouthRoom() != null)
                                {
                                    if (yy < height - 2)
                                        ObteBuffer().Print(xx, yy + 1, "|");
                                }
                                if (lvlLayout[i].GetWestRoom() != null)
                                {
                                    if (xx > width - 19)
                                        ObteBuffer().Print(xx - 1, yy, "-");
                                }
                                if (lvlLayout[i].GetEastRoom() != null)
                                {
                                    if (xx < width - 2)
                                        ObteBuffer().Print(xx + 1, yy, "-");
                                }
                            }
                        }
                    }
                }
            }
            ObteBuffer().Print(101, 0, "Planta: " + -(juego.level - 1));
        }
    }
}
