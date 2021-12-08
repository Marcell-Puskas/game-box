using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GameBox
{
    class Push_the_box
    {
        struct push_data
        {
            public ConsoleColor color;
            public int id;
        }
        public bool Push_the_box_game()
        {
            int mapx = 9;
            int mapy = 9;
            int posx = 0;
            int posy = 0;

            bool run = true;

            ConsoleKey key;

            //var Boxes = new List<string[]>();

            push_data[,] push_map = new push_data[mapx, mapy];
            Screen_data[,] screen_map = new Screen_data[mapx, mapy];

            //Border border = new Border();
            Screen screen = new Screen();

            if(File.Exists(@"F:\code\game-box\map1.txt"))
            using (StreamReader hs_read = new StreamReader(@"F:\code\game-box\map1.txt"))
            {
                for (int cy = 0; cy < mapy; cy++)
                {
                    string hs_line = hs_read.ReadLine();
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        switch(hs_line[cx])
                        {
                            case 'w':
                                push_map[cx, cy].color = ConsoleColor.Blue;
                                push_map[cx, cy].id = 1;
                            break;

                            case 'p':
                                posx = cx;
                                posy = cy;
                            break;

                            case 'b':
                                push_map[cx, cy].color = ConsoleColor.Yellow;
                                push_map[cx, cy].id = 2;
                            break;

                            case 's':
                                push_map[cx, cy].color = ConsoleColor.Green;
                                push_map[cx, cy].id = 3;
                            break;
                        }
                    }
                }
            }

            while(run)
            {

                for (int cy = 0; cy < mapy; cy++)
                {
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        screen_map[cx, cy].color = push_map[cx, cy].color;
                        switch(push_map[cx, cy].id)
                        {
                            case 1:
                            screen_map[cx, cy].screen_char = "W";
                            break;

                            case 2:
                            screen_map[cx, cy].screen_char = "B";
                            break;

                            case 3:
                            screen_map[cx, cy].screen_char = "S";
                            break;
                        }
                        if(push_map[cx, cy].id != 0) screen_map[cx, cy].visible = true;
                        else screen_map[cx, cy].visible = false;
                    }
                }

                screen_map[posx, posy].screen_char = "O";
                screen_map[posx, posy].color = ConsoleColor.Red;
                screen_map[posx, posy].visible = true;

                screen.Print(screen_map);


                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                    if(Check_move(-1, 0)) posx--;
                    break;

                    case ConsoleKey.D:
                    if(Check_move(1, 0)) posx++;
                    break;

                    case ConsoleKey.W:
                    if(Check_move(0, -1)) posy--;
                    break;

                    case ConsoleKey.S:
                    if(Check_move(0, 1)) posy++;
                    break;

                    case ConsoleKey.Escape:
                    return false;

                    case ConsoleKey.M:
                    return true;
                }

                
            }
            return false;
        }
        
        bool Check_move(int px, int py)
        {
            /* if (posx + px < 0) return false;
            else if (posx + px > mapx - 1) return false;
            else if (posy + py < 0) return false;
            else if (posy + py > mapy - 1) return false;

            else if (push_map[posx + px, posy + py].id == 1) return false;

            else if(push_map[posx + px, posy + py].id == 2)
            {
                if(push_map[posx + px * 2, posy + py * 2].id == 1) return false;
                else 
                {
                    int nextSpace = 2;
                    while (push_map[posx + px * nextSpace, posy + py * nextSpace].id == 2)
                    {
                        nextSpace++;
                    }
                    push_map[posx + px, posy + py].id = 0;
                    push_map[posx + px * nextSpace, posy + py * nextSpace].id = 2;
                    push_map[posx + px * namespace, posy + py * namespace].color = push_map[posx + px, posy + py].color;
                    return true;
                }
            }
            else  */return true;
        }
    }
}