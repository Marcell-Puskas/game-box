using System;
using System.Collections.Generic;

namespace GameBox
{
    class Program
    {
        static void Main(string[] args)
        {
            void call_game(string call_name)
            {
                switch(call_name)
                {
                    case "snake":
                    Snake snake = new Snake();
                    snake.Snake_game();
                    break;

                    case "tetris":
                    Tetris tetris = new Tetris();
                    tetris.Tetris_game();
                    break;
                }
            }

            string selected_game = null;

            string[] call_games = {
                    "snake",
                    "tetris"
            };

            string[] text_games = {
                    "Snake",
                    "Tetris (még nincs kész)"
            };

            if(args.Length >= 2)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if(args[i] == "-g" && i + 1 < args.Length)
                    {
                        call_game(args[i + 1]);
                    }
                }
            }

            Console.Clear();
            Console.CursorVisible = false;

            if(selected_game == null)
            {
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

                string[] text_title = {
                    "",
                    "Játékok:",
                    ""
                    };

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
                    
                    for (int i = 0; i < text_title.Length; i++)
                    {
                        Console.Write(border_vertical);
                        
                        Console.Write(" ");

                        for (int cx = 0; cx < mapx - 1; cx++)
                        {
                            if(text_title[i].Length > cx)
                            {
                                Console.Write(text_title[i][cx]);
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical + "\n");
                    }

                    for (int i = 0; i < text_games.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(border_vertical);

                        Console.Write(" ");

                        for (int cx = 0; cx < mapx - 1; cx++)
                        {
                            if(text_games[i].Length > cx)
                            {
                                if(selected_index == i)
                                {
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                                Console.Write(text_games[i][cx]);
                                Console.BackgroundColor = ConsoleColor.Black;
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

                    for (int cy = 0; cy < mapy - text_games.Length; cy++)
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
                        if(selected_index+1 < text_games.Length)
                        {
                            selected_index++;
                        }
                        break;

                        case "Enter":
                        call_game(call_games[selected_index]);
                        Console.Clear();
                        break;

                        case "Escape":
                        run = false;
                        break;
                    }
                }
            }
        }
    }
}