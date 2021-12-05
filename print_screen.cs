using System;
using System.IO;
namespace GameBox
{
    public struct Screen_data
    {
        public bool visible;
        public string screen_char;
        public ConsoleColor color;
    }
    class Screen
    {
        const string empty_char = " ";
        
        int mapx = 9;
        int mapy = 9;

        public void Print(Screen_data[,] map)
        {
            Border border = new Border();
            
            border.border_print(mapx, mapy);

            Console.SetCursorPosition(1, 1);

            int cmd_offset_x = 1;
            int cmd_offset_y = 1;
            
            for (int cy = 0; cy < mapy; cy++)
            {
                for (int cx = 0; cx < mapy; cx++)
                {
                    if(map[cx, cy].visible)
                    {
                        Console.SetCursorPosition(cx + cmd_offset_x, cy + cmd_offset_y);
                        Console.ForegroundColor = map[cx, cy].color;
                        Console.Write(map[cx, cy].screen_char);
                    }
                    else
                    {
                        Console.SetCursorPosition(cx + cmd_offset_x, cy + cmd_offset_y);
                        Console.Write(empty_char);
                    }
                }
            }
        }
    }
}