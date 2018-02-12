using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class CubicalMemory : GeneralProgrammingStuff
    { 
        class Cube
        {
            double rangeOfTheEdge = 1.0;
            System.Drawing.Color color = System.Drawing.Color.Gray;

            public bool IsFilled = false;
        }

        class Chunk
        {
            const int Height = 32, Width = 16, Length = 16;

            Cube[][][] cubes;

            public Chunk()
            {
                TripleDimIniter<Cube>(ref cubes, Chunk.Height, Chunk.Width, Chunk.Length);

                int FromHeight = 0, ToHeight = 10;
                AlgorithmicalGround(FromHeight, ToHeight);
            }

            /// <summary>
            /// Basically it's the Algorithm to display default non-hard-memored earth ground.
            /// You can make it more advanced for Plants, Craters and e.t.c.
            /// </summary>
            /// <param name="fromHeight"></param>
            /// <param name="toHeight"></param>
            void AlgorithmicalGround(int fromHeight, int toHeight)
            {
                for (int i = fromHeight; i < toHeight; i++)
                    for (int j = 0; j < Width; j++)
                        for (int k = 0; k < Length; k++)
                            cubes[i][j][k].IsFilled = true;
            }
        }

            //Кубик состоит из 6 граней, квадратов.
        }
}
