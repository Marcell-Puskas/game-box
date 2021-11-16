using System;
using System.IO;
namespace GameBox
{
    class Minesweeper
    {
        struct Minesweeper_data
        {
            public int near_bombs;
            public bool bomb_here;
            public bool visible;
        }
        public bool Minesweeper_game()
        {
            int mapx = 9;
            int mapy = 9;
            int posx, posy;
            int bomb_count = 10;
            const string char_bomb = "X";
            const string char_cursor = "O";
            bool run = true;

            Minesweeper_data[,] bomb_map = new Minesweeper_data[mapx, mapy];
            Screen_data[,] map = new Screen_data[mapx, mapy];
            Random cord = new Random();
            ConsoleKey key = new ConsoleKey();

            
            Border border = new Border();
            Screen screen = new Screen();

            Console.Clear();
            Console.CursorVisible = false;
            border.border_print(mapx, mapy);

            Console.Clear();

            for (int i = 0; i < bomb_count; i++)
            {
                do{
                    posx = cord.Next(0, mapx);
                    posy = cord.Next(0, mapy);
                }while(bomb_map[posx, posy].bomb_here);

                bomb_map[posx, posy].bomb_here = true;

                for (int cy = Math.Max(posy-1, 0); cy <= Math.Min(posy+1, mapy-1); cy++)
                {
                    for (int cx = Math.Max(posx-1, 0); cx <= Math.Min(posx+1, mapx-1); cx++)
                    {
                        bomb_map[cx, cy].near_bombs++;
                    }
                }
            }

            posx = 0;
            posy = 0;

            /* for (int cy = 0; cy < mapy; cy++)
            {
                for (int cx = 0; cx < mapy; cx++)
                {
                    if(bomb_map[cx, cy].bomb_here)
                    {
                        map[cx, cy].screen_char = char_bomb;
                        map[cx, cy].visible = true;
                    }
                    else
                    {
                        map[cx, cy].screen_char = Convert.ToString(bomb_map[cx, cy].near_bombs);
                        map[cx, cy].visible = true;
                    }
                }
            } */


            while(run)
            {
                for (int cy = 0; cy < mapy; cy++)
                {
                    for (int cx = 0; cx < mapy; cx++)
                    {
                        if(bomb_map[cx, cy].bomb_here && bomb_map[cx, cy].visible)
                        {
                            map[cx, cy].screen_char = char_bomb;
                            map[cx, cy].visible = true;
                        }
                        else if(bomb_map[cx, cy].visible)
                        {
                            map[cx, cy].screen_char = Convert.ToString(bomb_map[cx, cy].near_bombs);
                            map[cx, cy].visible = true;
                        }
                        else
                        {
                            map[cx, cy].visible = false;
                        }
                    }
                }

                map[posx, posy].screen_char = char_cursor;
                map[posx, posy].visible = true;
                
                key = Console.ReadKey().Key;

                switch(key)
                {
                    case ConsoleKey.A:
                    if(posx > 0) posx--;
                    break;

                    case ConsoleKey.D:
                    if(posx < mapx-1) posx++;
                    break;

                    case ConsoleKey.W:
                    if(posy > 0) posy--;
                    break;

                    case ConsoleKey.S:
                    if(posy < mapy-1) posy++;
                    break;

                    case ConsoleKey.Enter:
                    bomb_map[posx, posy].visible = true;
                    break;

                    case ConsoleKey.Escape:
                    return false;

                    case ConsoleKey.M:
                    return true;
                }

                screen.Print(map);
                Console.Write("x: {0}, y: {1}", posx, posy);
            }

            return true;
        }
    }
}