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
        public void Tetris_game()
        {
            int mapx = 10;
            int mapy = 40;

            string border_top_left = "╔";
            string border_top = "═";
            string border_top_right = "╗";
            string border_bottom_left = "╚";
            string border_bottom = "═";
            string border_bottom_right = "╝";
            string border_vertical = "║";

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

            bool run = true;
            bool gameover = false;

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
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(border_vertical + "\n");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(border_bottomline);

                Console.ReadKey();
                run = false;
            }
        }
    }
}
