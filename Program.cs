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

                    case "newtetris":
                    Newtetris newtetris = new Newtetris();
                    run = newtetris.New_tetris_game();
                    break;

                    case "minesweeper":
                    Minesweeper minesweeper = new Minesweeper();
                    run = minesweeper.Minesweeper_game();
                    break;

                    case "teszt1":
                    Test1 test1 = new Test1();
                    run = test1.test1_code();
                    break;

                    case "teszt2":
                    Test2 test2 = new Test2();
                    run = test2.test2_code();
                    break;

                    case "pushthebox":
                    Push_the_box push_The_Box = new Push_the_box();
                    run = push_The_Box.Push_the_box_game();
                    break;

                    case "tictactoe":
                    TicTacToe ticTacToe = new TicTacToe();
                    run = ticTacToe.tictactoe_game();
                    break;
                }
            }

            string selected_game = null;

            string[] call_games = {
                    "snake",
                    "newtetris",
                    "minesweeper",
                    "test1",
                    "test2",
                    "pushthebox",
                    "tictactoe"
            };

            string[] text_games = {
                    "Snake",
                    "Új Tetris",
                    "Aknakereső (nincs kész)",
                    "Teszt 1",
                    "Teszt 2",
                    "Push The Box",
                    "Amőba"
            };

            if(args.Length >= 2)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if(args[i] == "-g" && i + 1 < args.Length)
                    {
                        call_game(args[i + 1].ToLower());
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