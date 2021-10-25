using System;
using System.IO;

namespace GameBox
{
    class Teszt1
    {
        public bool teszt1_code()
        {
            Console.Clear();
            Console.WriteLine("teszt1");

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
            for (int i = 0; i < blocks.Length; i++)
            {
                Console.WriteLine(blocks[i].GetLength(0));
            }
            /*
            foreach (var item in blocks)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2);
                }
            }*/

            Console.ReadKey();
            return false;
        }
    }
}