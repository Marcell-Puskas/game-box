using System;
using System.IO;
namespace GameBox
{
    struct Minesweeper_data
    {
        public int near_bombs;
        public bool bomb_here;
        public bool visible;
    }
    class Minesweeper
    {
        public bool Minesweeper_game()
        {
            int mapx = 9;
            int mapy = 9;
            int posx, posy;
            int bomb_count = 10;

            bool run = true;

            Minesweeper_data[,] map = new Minesweeper_data[mapx, mapy];
            Random cord = new Random();
            ConsoleKey key = new ConsoleKey();

            Console.Clear();

            for (int i = 0; i < bomb_count; i++)
            {
                do{
                    posx = cord.Next(0, mapx);
                    posy = cord.Next(0, mapy);
                }while(map[posx, posy].bomb_here);

                map[posx, posy].bomb_here = true;

                for (int cy = Math.Max(posy-1, 0); cy <= Math.Min(posy+1, mapy-1); cy++)
                {
                    for (int cx = Math.Max(posx-1, 0); cx <= Math.Min(posx+1, mapx-1); cx++)
                    {
                        map[cx, cy].near_bombs++;
                    }
                }
            }

            posx = 0;
            posy = 0;

            /*for (int cy = 0; cy < mapy; cy++)
            {
                for (int cx = 0; cx < mapy; cx++)
                {
                    if(map[cx, cy].bomb_here)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(map[cx, cy].near_bombs);
                    }
                }
                Console.WriteLine();
            }*/

            while(run)
            {
                key = Console.ReadKey().Key;
                Console.WriteLine(key);

                switch(key)
                {
                    case ConsoleKey.A:
                    if(posx < 0) posx--;
                    break;

                    case ConsoleKey.D:
                    if(posx > mapx-1) posx++;
                    break;

                    case ConsoleKey.W:
                    if(posy < 0) posy--;
                    break;

                    case ConsoleKey.S:
                    if(posy > mapy-1) posy++;
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