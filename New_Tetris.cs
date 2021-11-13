using System;
using System.IO;
using System.Threading;

namespace GameBox
{
    class Newtetris
    {
        int[,,] tetro_cordinates = {
                {{0, 1}, {1, 1}, {2, 1}, {3, 1}},
                {{0, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{2, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{0, 0}, {1, 0}, {0, 1}, {1, 1}},
                {{1, 0}, {2, 0}, {0, 1}, {1, 1}},
                {{1, 0}, {0, 1}, {1, 1}, {2, 1}},
                {{0, 0}, {1, 0}, {1, 1}, {2, 1}}
            };

        int[,] tetro_offsets = {
                {3, 3},
                {2, 2},
                {2, 2},
                {1, 1},
                {2, 2},
                {2, 2},
                {2, 2}
            };

        ConsoleColor[] tetro_colors = {
            ConsoleColor.Blue,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.DarkRed,
            ConsoleColor.Red
        };
        const ConsoleColor full_line_color = ConsoleColor.White;
        const ConsoleColor background_color = ConsoleColor.White;
        const string char_mino = "O";
        const string char_full_line = "X";
        const string char_background = " ";
        const string text_score = "Score: ";
        const int mapx = 8;
        const int mapy = 16;
        const int cmd_offset_x = 1;
        const int cmd_offset_y = 1;
        const int tetro_num = 7;
        const int mino_num = 4;
        const int clear_time = 400;
        const int score_time = 2000;
        Random blockrandom = new Random();
        Border border = new Border();

        bool run, gameover, menu, printing;

        int posx, posy, dir, selected_index, points, speed, timeout_x, timeout_y, timeout_b;

        char keychar, movechar;

        int[,] selected_tetro;
        int[,] construncted_tetro;
        int[,] stack_map;
        struct Tetris_data
        {
            public bool block;
            public ConsoleColor color;
        }
        public bool New_tetris_game()
        {
            selected_index = blockrandom.Next(tetro_num);

            stack_map = new int[mapx, mapy];
            selected_tetro = new int[mino_num, 2];
            construncted_tetro = new int[mino_num, 2];

            Console.Clear();
            Console.CursorVisible = false;

            border.border_print(mapx, mapy);

            points = 0;
            speed = 500;
            run = true;
            gameover = false;

            New_tetro();

            Thread key_getThread = new Thread(new ThreadStart(Key_get));
            Thread logicThread = new Thread(new ThreadStart(Logic));
            
            key_getThread.Start();
            logicThread.Start();
            
            while(run) Thread.Sleep(100);

            key_getThread.Join();
            logicThread.Join();
            
            if(gameover)
            {
                Thread.Sleep(score_time);
            }
            return menu;
        }

        public void Key_get()
        {
            while(run)
            {
                string key = "";
                string move = "";
                key = Convert.ToString(Console.ReadKey().Key);

                switch(key)
                {
                    case "LeftArrow": move = "l"; break;
                    case "RightArrow": move = "r"; break;
                    case "UpArrow": move = "u"; break;
                    case "DownArrow": move = "d"; break;
                    case "Escape": run = false; menu = false; break;
                    case "M": run = false; menu = true; break;
                }

                switch(move)
                { 
                    case "l": if(check_move(posx - 1, posy, dir)) posx--; break;
                    case "r": if(check_move(posx + 1, posy, dir)) posx++; break;
                    case "d": if(check_move(posx, posy + 1, dir)) posy++; break;

                    case "u":
                    if(check_move(posx, posy, (dir + 1) % 4))
                    {
                        dir = (dir + 1) % 4;
                        Construct_tetro(dir, true);
                    }
                    break;
                }
                Screen_print();
            }
        }
  
        void Construct_tetro(int cdir, bool select = false)
        {
            for (int cmino = 0; cmino < mino_num; cmino++)
            {
                switch (cdir)
                {
                    case 0:
                    construncted_tetro[cmino, 0] = tetro_cordinates[selected_index, cmino, 0];
                    construncted_tetro[cmino, 1] = tetro_cordinates[selected_index, cmino, 1];
                    break;

                    case 1:
                    construncted_tetro[cmino, 0] = tetro_offsets[selected_index, 0] - tetro_cordinates[selected_index, cmino, 1];
                    construncted_tetro[cmino, 1] = tetro_cordinates[selected_index, cmino, 0];
                    break;

                    case 2:
                    construncted_tetro[cmino, 0] = tetro_offsets[selected_index, 0] - tetro_cordinates[selected_index, cmino, 0];
                    construncted_tetro[cmino, 1] = tetro_offsets[selected_index, 1] - tetro_cordinates[selected_index, cmino, 1];
                    break;

                    case 3:
                    construncted_tetro[cmino, 0] = tetro_cordinates[selected_index, cmino, 1];
                    construncted_tetro[cmino, 1] = tetro_offsets[selected_index, 1] - tetro_cordinates[selected_index, cmino, 0];
                    break;
                }
            }
            if(select)
            {
                for (int cmino = 0; cmino < mino_num; cmino++)
                {
                    selected_tetro[cmino, 0] = construncted_tetro[cmino, 0];
                    selected_tetro[cmino, 1] = construncted_tetro[cmino, 1];
                }
            }
        }

        void New_tetro()
        {
            dir = 0;
            selected_index = blockrandom.Next(tetro_num);
            Construct_tetro(dir, true);

            posx = mapx / 2 - tetro_offsets[selected_index, 0] / 2;
            if(tetro_offsets[selected_index, 1] == 3) posy = -1;
            else posy = 0;
        }

        void Screen_print()
        {
            if(!printing)
            {
                printing = true;
                //draw stack
                for (int cy = 0; cy < mapy; cy++)
                {
                    for (int cx = 0; cx < mapx; cx++)
                    {
                        if(stack_map[cx, cy] != 0)
                        {
                            Console.SetCursorPosition(cx + cmd_offset_x, cy + cmd_offset_y);
                            Console.ForegroundColor = tetro_colors[stack_map[cx, cy] - 1];
                            Console.Write(char_mino);
                        }
                        else
                        {
                            Console.SetCursorPosition(cx + cmd_offset_x, cy + cmd_offset_y);
                            Console.ForegroundColor = background_color;
                            Console.Write(char_background);
                        }
                    }
                }

                //draw current block
                for (int cmino = 0; cmino < mino_num; cmino++)
                {
                    Console.SetCursorPosition(
                        posx + selected_tetro[cmino, 0] + cmd_offset_x,
                        posy + selected_tetro[cmino, 1] + cmd_offset_y);
                    Console.ForegroundColor = tetro_colors[selected_index];
                    Console.Write(char_mino);
                }

                
                Console.SetCursorPosition(0, mapy + 2);
                Console.Write(text_score);
                Console.WriteLine(points);
                
                printing = false;
            }
        }

        bool check_move(int nextX, int nextY, int nextDir)
        {
            Construct_tetro(nextDir);
            for(int cmino = 0; cmino < mino_num; cmino++)
            {
                if(nextX + construncted_tetro[cmino, 0] >= mapx) return false;
                if(nextX + construncted_tetro[cmino, 0] < 0) return false;
                if(nextY + construncted_tetro[cmino, 1] >= mapy) return false;
                if(nextY + construncted_tetro[cmino, 1] < 0) return false;

                if(stack_map[ nextX + construncted_tetro[cmino, 0] ,  nextY + construncted_tetro[cmino, 1] ] != 0)
                    return false;
            }
            return true;
        }

        void Check_full_line()
        {
            bool lines_to_clear = false;
            for (int cline = 0; cline < mapy; cline++)
            {
                bool line_full = true;
                for (int crow = 0; crow < mapx; crow++)
                    if(stack_map[crow, cline] == 0)
                        line_full = false;
                
                if(line_full)
                {
                    Console.SetCursorPosition(cmd_offset_x, cmd_offset_y + cline);
                    for(int cx = 0; cx < mapx; cx++)
                    {
                        Console.ForegroundColor = full_line_color;
                        Console.Write(char_full_line);
                    }
                    
                    lines_to_clear = true;
                    points++;

                    for (int copyy = cline; copyy >= 1; copyy--)
                            for (int copyx = 0; copyx < mapx; copyx++)
                                stack_map[copyx, copyy] = stack_map[copyx, copyy - 1];
                }
            }

            if(lines_to_clear)
            {
                Thread.Sleep(clear_time);
            }
        }

        void Check_gameover()
        {
            if (!check_move(posx, posy, dir))
            {
                gameover = true;
                run = false;
            }
        }

        void Logic()
        {
            while(run)
            {
                if(check_move(posx, posy + 1, dir))
                {
                    posy += 1;
                }
                else
                {
                    for(int cmino = 0; cmino < mino_num; cmino++)
                    {
                        stack_map[ posx + selected_tetro[cmino, 0],  posy + selected_tetro[cmino, 1] ] = selected_index + 1;
                    }
                    Check_full_line();
                    New_tetro();
                    Check_gameover();
                }

                Screen_print();
                Thread.Sleep(speed);
            }
        }

    }
}
