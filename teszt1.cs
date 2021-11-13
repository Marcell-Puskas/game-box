using System;
using System.IO;

namespace GameBox
{
    class Test1
    {
        public bool test1_code()
        {
            Console.Clear();

            int[,,] Tetro_cordinates = {
                {{0, 1}, {1, 1}, {2, 1}, {3, 1}},
                {{0, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{2, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{0, 0}, {1, 0}, {0, 1}, {1, 1}},
                {{1, 0}, {2, 0}, {0, 1}, {1, 1}},
                {{1, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{0, 0}, {1, 0}, {1, 1}, {2, 1}}
            };

            int[,] Tetro_offsets = {
                {3, 3},
                {2, 2},
                {2, 2},
                {1, 1},
                {2, 2},
                {2, 2},
                {2, 2}
            };

            int[,] c_tetro = new int[4, 2];

            int cmd_offset_x = 5;
            int cmd_offset_y = 5;

            for (int cdir = 0; cdir < 4; cdir++)
            {
                for (int ctetro = 0; ctetro < 7; ctetro++)
                {
                    for (int cmino = 0; cmino < 4; cmino++)
                    {
                        switch (cdir)
                        {
                            case 0:
                            c_tetro[cmino, 0] = Tetro_cordinates[ctetro, cmino, 0];
                            c_tetro[cmino, 1] = Tetro_cordinates[ctetro, cmino, 1];
                            break;

                            case 1:
                            c_tetro[cmino, 0] = Tetro_offsets[ctetro, 0] - Tetro_cordinates[ctetro, cmino, 1];
                            c_tetro[cmino, 1] = Tetro_cordinates[ctetro, cmino, 0];
                            break;

                            case 2:
                            c_tetro[cmino, 0] = Tetro_offsets[ctetro, 0] - Tetro_cordinates[ctetro, cmino, 0];
                            c_tetro[cmino, 1] = Tetro_offsets[ctetro, 1] - Tetro_cordinates[ctetro, cmino, 1];
                            break;

                            case 3:
                            c_tetro[cmino, 0] = Tetro_cordinates[ctetro, cmino, 1];
                            c_tetro[cmino, 1] = Tetro_offsets[ctetro, 1] - Tetro_cordinates[ctetro, cmino, 0];
                            break;
                        }
                    }

                    Console.SetCursorPosition(
                            cmd_offset_x * cdir, 
                            cmd_offset_y * ctetro);
                        Console.Write('X');

                    for (int cmino = 0; cmino < 4; cmino++)
                    {
                        Console.SetCursorPosition(
                            c_tetro[cmino, 0] + cmd_offset_x *cdir + 1, 
                            c_tetro[cmino, 1] + cmd_offset_y * ctetro + 1);
                        Console.Write('O');
                    }
                } 
            }

            

            if(Console.ReadKey().Key == ConsoleKey.M)
                return true;
            else return false;
        }
    }
}