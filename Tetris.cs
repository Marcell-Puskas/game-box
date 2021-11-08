using System;
using System.IO;
using System.Threading;

namespace GameBox
{
    class Tetris
    {
        const int mapx = 10;
        const int mapy = 40;

        int posx = 0;
        int posy = 0;
        Random blockrandom = new Random();
        Border border = new Border();
        int current_block;
        Tetris_data[,] map;
        bool run = true;
        bool gameover = false;
        bool menu = true;
        bool printing = false;
        int[][,] blocks = new int[][,]
        {
            new int[,] { 
                {1, 1, 1, 1}, 
            },
            new int[,] { 
                {1, 0, 0},
                {1, 1, 1}
            },
            new int[,] {
                {0, 0, 1},
                {1, 1, 1}
            },
            new int[,] {
                {1, 1,},
                {1, 1}
            },
            new int[,] {
                {0, 1, 1},
                {1, 1, 0}
            },
            new int[,] {
                {0, 1, 0},
                {1, 1, 1}
            },
            new int[,] {
                {1, 1, 0},
                {0, 1, 1}
            }
        };

        ConsoleColor[] block_color = {
            
        };

        struct Tetris_data
        {
            public bool block;
            public ConsoleColor color;
        }

        public bool Tetris_game()
        {
            current_block = blockrandom.Next(0, blocks.Length - 1);

            map = new Tetris_data[mapx, mapy];

            Console.Clear();
            Console.CursorVisible = false;

            border.border_print(mapx, mapy);

            /*for (int i = 0; i < 10; i++)
            {
                map[5, i * 2].block = true;
            }*/

            Thread key_getThread = new Thread(new ThreadStart(Key_get));
            key_getThread.Start();
            Thread logicThread = new Thread(new ThreadStart(Logic));
            logicThread.Start();
            while(run) Thread.Sleep(200);
            key_getThread.Join();
            logicThread.Join();
            return menu;
        }
        public void Screen_print()
        {
            if(!printing)
            {
                printing = true;
                for (int cy = 0; cy < mapy; cy++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(1, cy + 1);
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        if(posx <= cx && cx < posx + blocks[current_block].GetLength(1) && posy <= cy && cy < posy + blocks[current_block].GetLength(0))
                        {
                            if (blocks[current_block][cy - posy, cx - posx] == 1)
                            {
                                Console.Write("X");
                            }
                            else
                            {
                                Console.Write(".");
                            }
                        }
                        else if(map[cx, cy].block)
                        {
                            //Console.ForegroundColor = map[cx, cy].color;
                            Console.Write("O");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                printing = false;
            }
        }
        public void Key_get()
        {
            string key = "";
            string move = "";
            while(run && !gameover)
            {
                key = Convert.ToString(Console.ReadKey().Key);

                switch(key)
                {
                    case "LeftArrow":
                    move = "l";
                    break;

                    case "RightArrow":
                    move = "r";
                    break;

                    case "UpArrow":
                    move = "u";
                    break;

                    case "DownArrow":
                    move = "d";
                    break;

                    case "Escape":
                    run = false;
                    menu = false;
                    break;

                    case "M":
                    run = false;
                    menu = true;
                    break;
                }

                switch(move)
                {
                    case "l":
                    if(check_move(posx - 1, posy)) posx--;
                    break;

                    case "r":
                    if(check_move(posx + 1, posy)) posx++;
                    break;

                    case "d":
                    if(check_move(posx, posy + 1)) posy++;
                    break;

                    case "u":
                    
                    break;
                }

                Screen_print();
            }
        }

        public bool check_move(int nextX, int nextY)
        {
            for(int cy = 0; cy < blocks[current_block].GetLength(0); cy++)
            {
                for(int cx = 0; cx < blocks[current_block].GetLength(1); cx++)
                {
                    if(nextX < 0) return false;
                    else if(nextX + blocks[current_block].GetLength(1) > mapx) return false;
                    else if(nextY + blocks[current_block].GetLength(0) > mapy) return false;
                    else if(map[cx + nextX, cy + nextY].block && blocks[current_block][cy, cx] == 1) return false;
                }
            }
            return true;
        }
        public void Logic()
        {
            while(run && !gameover)
            {
                if(check_move(posx, posy + 1))
                {
                    posy += 1;
                }
                else
                {
                    for(int cy = 0; cy < blocks[current_block].GetLength(0); cy++)
                    {
                        for(int cx = 0; cx < blocks[current_block].GetLength(1); cx++)
                        {
                            if(blocks[current_block][cy, cx] == 1)
                            {
                                map[posx + cx, posy + cy].block = true;
                            }
                        }
                    }

                    posx = 0;
                    posy = 0;

                    current_block = blockrandom.Next(0, blocks.Length - 1);
                }

                Screen_print();

                Thread.Sleep(500);
            }
        }
    }
}
