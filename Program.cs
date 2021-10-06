using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string selected_game = null;

            if(args.Length >= 2)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if(args[i] == "-g" && i + 1 < args.Length)
                    {
                        selected_game = args[i + 1];
                    }
                }
            }

            if(selected_game == null)
            {
                string[] games = {
                    "Snake",
                    "test1",
                    "test2"
                    };

                string[,] game_list = {
                    {"Snake" }
                };
                
                string[] text_games = {
                    "",
                    "Játékok:",
                    ""
                    };

                int selected_index = 0;

                int mapx = 40;
                int mapy = 20;

                string border_top_left = "╔";
                string border_top = "═";
                string border_top_right = "╗";
                string border_bottom_left = "╚";
                string border_bottom = "═";
                string border_bottom_right = "╝";
                string border_vertical = "║";

                string key = "";
                bool run = true;

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

                while(run)
                {
                    Console.SetCursorPosition(0, 0);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_topline);
                    
                    for (int i = 0; i < text_games.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical);

                        Console.Write(" ");

                        for (int cx = 0; cx < mapx - 1; cx++)
                        {
                            if(text_games[i].Length > cx)
                            {
                                Console.Write(text_games[i][cx]);
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical + "\n");
                    }

                    for (int i = 0; i < games.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical);

                        Console.Write(" ");

                        for (int cx = 0; cx < mapx - 1; cx++)
                        {
                            if(games[i].Length > cx)
                            {
                                if(selected_index == i)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                Console.Write(games[i][cx]);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(" ");
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical + "\n");
                    }

                    for (int cy = 0; cy < mapy - games.Length; cy++)
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
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_bottomline);

                    key = Convert.ToString(Console.ReadKey(true).Key);

                    switch(key)
                    {
                        case "UpArrow":
                        if(selected_index != 0)
                        {
                            selected_index--;
                        }
                        break;
                        
                        case "DownArrow":
                        if(selected_index+1 < games.Length)
                        {
                            selected_index++;
                        }
                        break;

                        case "Escape":
                        run = false;
                        break;
                    }
                }
            }
            Snake snake = new Snake();
            snake.Snake_game();
        }
    }
}