using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleApp1
{
    struct Snake
    {
        public string id;
        public double age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            //Console.CursorVisible = false;

            //config
            int mapx = 40;
            int mapy = 20;
            int def_length = 3;
            string text_info = "Mozgás: W A S D, kipépés: Escape";
            string text_gameover = "Játék vége";
            string text_score = "Pontszám:";
            string text_name = "Írd be a neved a pontszám elmentéséhez!\nNeved: ";

            string char_snake = "O";
            string char_dead_snake = "X";
            string char_food = "H";
            string char_background = " ";

            string border_top_left = "╔";
            string border_top = "═";
            string border_top_right = "╗";
            string border_bottom_left = "╚";
            string border_bottom = "═";
            string border_bottom_right = "╝";
            string border_vertical = "║";

            int posx = 5;
            int posy = 5;
            int snakelength = 0;

            bool screen_warp = false;


            Snake[,] map = new Snake[mapx, mapy];
            Random foodr = new Random();
            var highscore = new List<string[]>();

            using (StreamReader hs_read = new StreamReader(@".\highscore.txt"))
            {
                string hs_r_line = "";
                hs_r_line = hs_read.ReadLine();
                do
                {
                    highscore.Add(hs_r_line.Split(";"));
                    hs_r_line = hs_read.ReadLine();
                }
                while(hs_r_line != null);
            }

            foreach (var item in highscore)
            {
                Console.WriteLine(item[0]);
                Console.WriteLine(item[1]);
            }
            Console.ReadKey();

            StreamWriter hs_write = new StreamWriter(@".\highscore.txt");

            int foodx = foodr.Next(0, mapx);
            int foody = foodr.Next(0, mapy);
            int dir = 0;
            bool run = true;
            bool gameover = false;
            bool menu = true;
            string key = "";
            string screen = "";
            string snsake_screen = "";
            string second_key = "";
            string current_snake_char = char_snake;

            var keys = new List<string>();

            string border_topline = border_top_left;
            for (int cx = 0; cx <= mapx - 1; cx++)
            {
                border_topline += border_top;
            }
            border_topline += border_top_right + "\n";

            string border_bottomline = border_bottom_left;
            for (int cx = 0; cx <= mapx - 1; cx++)
            {
                border_bottomline += border_bottom;
            }
            border_bottomline += border_bottom_right + "\n";

            Console.CursorVisible = false;

            while(false)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(border_topline);

                for (int cy = 0; cy < mapy; cy++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical);

                    
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical + "\n");
                }
                
                Console.Write(border_bottomline);
            }

            while (run)
            {
                if (Console.KeyAvailable)
                {
                    while (Console.KeyAvailable)
                    {
                        keys.Add(Convert.ToString(Console.ReadKey(true).Key));
                    }
                }
                else
                {
                    key = second_key;
                    second_key = "";
                }

                if (keys.Count == 1)
                {
                    key = keys[^1];
                }
                if (keys.Count >= 2)
                {
                    second_key = keys[^1];
                    key = keys[^2];
                }

                keys.Clear();

                switch (key)                                        //billentyűzet bemenet
                {
                    case "D":
                        if (dir != 1) dir = 0;
                        break;
                    case "RightArrow":
                        if (dir != 1) dir = 0;
                        break;

                    case "A":
                        if (dir != 0) dir = 1;
                        break;
                    case "LeftArrow":
                        if (dir != 0) dir = 1;
                        break;

                    case "S":
                        if (dir != 3) dir = 2;
                        break;
                    case "DownArrow":
                        if (dir != 3) dir = 2;
                        break;

                    case "W":
                        if (dir != 2) dir = 3;
                        break;
                    case "UpArrow":
                        if (dir != 2) dir = 3;
                        break;

                    case "Escape":
                        run = false;
                        break;
                }

                switch (dir)                                         //irány
                {
                    case 0:
                        if (posx + 1 >= mapx) posx = 0;
                        else posx++;
                        break;
                    case 1:
                        if (posx <= 0) posx = mapx - 1;
                        else posx--;
                        break;
                    case 2:
                        if (posy + 1 >= mapy) posy = 0;
                        else posy++;
                        break;
                    case 3:
                        if (posy <= 0) posy = mapy - 1;
                        else posy--;
                        break;
                }

                if (map[posx, posy].id == "snake")
                {
                    run = false;
                    gameover = true;
                    current_snake_char = char_dead_snake;
                }

                map[posx, posy].id = "snake";
                map[posx, posy].age = 1;

                if (posx == foodx && posy == foody)
                {
                    foodx = foodr.Next(0, mapx);
                    foody = foodr.Next(0, mapy);
                    snakelength++;
                }
                else
                    map[foodx, foody].id = "food";


                Console.SetCursorPosition(0, 0);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(border_topline);

                for (int cy = 0; cy < mapy; cy++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical);
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        if (map[cx, cy].id == "snake")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(current_snake_char);
                            map[cx, cy].age++;
                            if (map[cx, cy].age > snakelength + def_length)
                            {
                                map[cx, cy].id = "air";
                                map[cx, cy].age = 0;
                            }
                        }

                        else if (map[cx, cy].id == "food")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(char_food);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(char_background);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical + "\n");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(border_bottomline);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(text_info + "\n" + text_score + snakelength);
                //Console.WriteLine("\n\n" + key + "\n\n" + second_key);//teszteléshez
                screen = "";
                Thread.Sleep(100 + 300 / (snakelength + 1));
            }
            if (gameover)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + text_gameover);
                Console.Write(text_name);
                string name = Console.ReadLine();
                //highscore.WriteLine();
            }
        }
    }
}