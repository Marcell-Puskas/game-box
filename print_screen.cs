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
            Console.SetCursorPosition(0, 0);
            
            for (int cy = 0; cy < mapy; cy++)
            {
                for (int cx = 0; cx < mapy; cx++)
                {
                    if(map[cx, cy].visible)
                    {
                        Console.Write(map[cx, cy].screen_char);
                    }
                    else
                    {
                        Console.Write(empty_char);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}