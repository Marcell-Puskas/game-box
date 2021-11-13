using System;
using System.IO;

namespace GameBox
{
    class Test2
    {
        public bool test2_code()
        {
            Random r = new Random();
            
            int mapx = 8;
            int mapy = 16;

            Border border = new Border();
            border.border_print(mapx, mapy);
            
            drawRect(1, 4, 2, 8);
            drawRect(5, 4, 2, 8);

            Console.WriteLine(r.Next(mapx));
            Console.ReadKey();
            return false;
        }
        public void drawRect(int x, int y, int width, int height)
        {

        }
    }
}