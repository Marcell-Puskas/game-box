using System;

namespace GameBox
{
    class TicTacToe
    {
        public bool tictactoe_game()
        {
            int mapx = 3;
            int mapy = 3;

            int sx = 0;
            int sy = 0;   

            char[,] tic_map = new char[mapx, mapy];
            tic_map[0, 0] = 'X';
            tic_map[1, 1] = 'O';


            bool run = true;
            ConsoleKey key = new ConsoleKey();

            Console.Clear();

            while(run)
            {
                Console.SetCursorPosition(0, 0);
                for (int cy = 0; cy < mapy; cy++)
                {
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        if(cx == sx && cy == sy)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(tic_map[cx, cy]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"sx: {sx}   sy: {sy}");

                key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.UpArrow:
                    if(sy > 0) sy--;
                    break;

                    case ConsoleKey.DownArrow:
                    if(sy < mapy - 1) sy++;
                    break;

                    case ConsoleKey.LeftArrow:
                    if(sx > 0) sx--;
                    break;

                    case ConsoleKey.RightArrow:
                    if(sx < mapx - 1) sx++;
                    break;

                    case ConsoleKey.Escape:
                    return false;

                    case ConsoleKey.M:
                    return true;
                }
            }



            return true;
        }
    }
}