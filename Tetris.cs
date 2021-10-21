using System;
using System.IO;

namespace GameBox
{
    class Tetris
    {
        struct Tetris_data
        {
            public int id;
            public string color;
        }
        public bool Tetris_game()
        {
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

            int mapx = 10;
            int mapy = 40;

            int posx = 0;
            int posy = 0;

            string border_top_left = "╔";
            string border_top = "═";
            string border_top_right = "╗";
            string border_bottom_left = "╚";
            string border_bottom = "═";
            string border_bottom_right = "╝";
            string border_vertical = "║";

            Random blockrandom = new Random();
            int current_block = blockrandom.Next(0, blocks.Length - 1);
            
            bool run = true;
            bool gameover = false;

            Tetris_data[,] map = new Tetris_data[mapx, mapy];

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

            Console.Clear();
            while (run && !gameover)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(border_topline);

                for (int cy = 0; cy < mapy; cy++)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical);
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
                    Console.Write(border_vertical + "\n");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(border_bottomline);

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    run = false;
                }
            }
            return false;
        }
    }
}
