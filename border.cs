using System;

namespace GameBox
{
    class Border
    {
        public void border_print(int mapx, int mapy)
        {
            string border_top_left = "╔";
            string border_top = "═";
            string border_top_right = "╗";
            string border_bottom_left = "╚";
            string border_bottom = "═";
            string border_bottom_right = "╝";
            string border_vertical = "║";

            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(0, 0);

            Console.Write(border_top_left);
            for (int cx = 0; cx <= mapx - 1; cx++)
            {
                Console.Write(border_top);
            }
            Console.WriteLine(border_top_right);

            for (int cy = 0; cy <= mapy - 1; cy++)
            {
                
                Console.SetCursorPosition(0, cy + 1);
                Console.Write(border_vertical);
                Console.SetCursorPosition(mapx + 1, cy + 1);
                Console.WriteLine(border_vertical);
            }

            Console.Write(border_bottom_left);
            for (int cx = 0; cx <= mapx - 1; cx++)
            {
                Console.Write(border_bottom);
            }
            Console.WriteLine(border_bottom_right);

            Console.SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}