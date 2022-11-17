using System;

namespace GameBox
{
    class TicTacToe
    {
        public bool tictactoe_game()
        {
            const int mapx = 3;
            const int mapy = 3;
            const int match_len = 3;
            const char Xchar = 'X';
            const char Ochar = 'O';
            const char Nchar = ' ';

            int sx = 0;
            int sy = 0;   

            char[,] tic_map = new char[mapx, mapy];
            for (int i = 0; i < mapy; i++) for (int j = 0; j < mapx; j++) tic_map[i, j] = Nchar;

            char selected = 'X';
            char winchar = ' ';
            char matchchar = ' ';

            bool match;
            bool win = false;

            int[,,] match3 = {
                {{0, 0}, {1, 0}, {2, 0}},
                {{0, 0}, {0, 1}, {0, 2}},
                {{0, 0}, {1, 1}, {2, 2}},
                {{0, 2}, {1, 1}, {2, 0}}
            };

            int[,] match3_lengs = {{2, 0}, {0, 2}, {2, 2}, {2, 2}};

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

                        Console.Write(tic_map[cx, cy]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
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

                    case ConsoleKey.Spacebar:
                    if(tic_map[sx, sy] == Nchar) 
                    {
                        tic_map[sx, sy] = selected;
                        if(selected == Xchar) selected = Ochar;
                        else selected = Xchar;
                    }
                    break;

                    case ConsoleKey.Escape:
                    return false;

                    case ConsoleKey.M:
                    return true;
                }

                for (int cm = 0; cm < match3.GetLength(0); cm++)
                {
                    for (int cy = 0; cy < mapy - match3_lengs[cm, 1]; cy++)
                    {
                        for (int cx = 0; cx < mapx - match3_lengs[cm, 0]; cx++)
                        {
                            match = true;
                            for (int cc = 0; cc < match3.GetLength(1); cc++)
                            {
                                if(matchchar == Nchar) matchchar = tic_map[ cx + match3[cm, cc, 0], cy + match3[cm, cc, 1] ];
                                else
                                {
                                    if (tic_map[ cx + match3[cm, cc, 0], cy + match3[cm, cc, 1] ] != matchchar)
                                    {
                                        match = false;
                                    }
                                }
                            }
                            if(match) 
                            {
                                win = true;
                                winchar = matchchar;
                            }
                        }
                    }
                }
                if(win) run = false;
            }
            if(win)
            {
                Console.WriteLine($"Win: {winchar}");
                Console.ReadKey();
            }


            return true;
        }
    }
}