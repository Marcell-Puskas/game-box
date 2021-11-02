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
            public int id;
            public ConsoleColor color;
        }

        public bool Tetris_game()
        {
            current_block = blockrandom.Next(0, blocks.Length - 1);

            map = new Tetris_data[mapx, mapy];

            Console.Clear();

            border.border_print(mapx, mapy);

            Thread key_getThread = new Thread(new ThreadStart(Key_get));
            key_getThread.Start();
            Thread logicThread = new Thread(new ThreadStart(Logic));
            logicThread.Start();
            while(run);
            key_getThread.Join();
            logicThread.Join();
            return menu;
        }
        public void Screen_print()
        {
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
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
        public void Key_get()
        {
            string key = "";
            while(run && !gameover)
            {
                key = Convert.ToString(Console.ReadKey().Key);

                switch(key)
                {
                    case "Escape":
                    run = false;
                    menu = false;
                    break;

                    case "M":
                    run = false;
                    menu = true;
                    break;
                }

                Screen_print();
            }
        }
        public void Logic()
        {
            while(run && !gameover)
            {
                posy += 1;

                Screen_print();

                Thread.Sleep(200);
            }
        }
    }
}
