using System;
using System.Collections.Generic;

namespace GameBox
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;

            void call_game(string call_name)
            {
                switch(call_name)
                {
                    case "snake":
                    Snake snake = new Snake();
                    run = snake.Snake_game();
                    break;

                    case "tetris":
                    Tetris tetris = new Tetris();
                    run = tetris.Tetris_game();
                    break;

                    case "teszt1":
                    Teszt1 teszt1 = new Teszt1();
                    run = teszt1.teszt1_code();
                    break;
                }
            }

            string selected_game = null;

            string[] call_games = {
                    "snake",
                    "tetris",
                    "teszt1"
            };

            string[] text_games = {
                    "Snake",
                    "Tetris (még nincs kész)",
                    "teszt program"
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

                Border border = new Border();

                string key = "";

                string[] text_title = {
                    "",
                    "Játékok:",
                    ""
                    };

                while(run)
                {
                    Console.SetCursorPosition(1, 1);

                    border.border_print(mapx, mapy);
                    
                    for (int i = 0; i < text_title.Length; i++)
                    {
                        Console.SetCursorPosition(1, i + 1);
                        
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
                    }

                    for (int i = 0; i < text_games.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(1, i + text_title.Length + 1);

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
                    }

                    for (int cy = 0; cy < mapy - text_games.Length - text_title.Length; cy++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(1, cy + text_title.Length + text_games.Length + 1);

                        for (int cx = 0; cx < mapx; cx++)
                        {
                            Console.Write(" ");
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

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
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }
}