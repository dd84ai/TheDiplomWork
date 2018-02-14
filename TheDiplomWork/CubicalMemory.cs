﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class CubicalMemory : GeneralProgrammingStuff
    {
        //Flat Ground from height to height
        static int FromHeight = 8, ToHeight = 10;

        //World beginning
        //Логично будет начинатся в углу и в низу.
        static Point3D Point_of_static_beginning = new Point3D(0, 0, 0);

        //Player range of view
        //From 0(only current) to more than 0.
        static int Range_of_view_static = 2;

        //Количество чанков в мире NxN
        static int Quantity_of_chunks_static = 100;

        public class Cube
        {
            public const double rangeOfTheEdge = 1.0;
            System.Drawing.Color color = System.Drawing.Color.Gray;

            public bool IsFilled = false;
        }

        public class Chunk
        {
            public const int Height = 32, Width = 10, Length = 10;

            Cube[][][] cubes;

            public Chunk()
            {
                TripleDimIniter<Cube>(ref cubes, Chunk.Height, Chunk.Width, Chunk.Length);

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



        class World
        {
            Point3D Point_of_beginning = new Point3D(Point_of_static_beginning);

            int Range_of_view = Range_of_view_static;

            List<List<Chunk>> World_as_Whole = new List<List<Chunk>>();
            List<List<Chunk>> World_as_DynamicLoaded = new List<List<Chunk>>();

            //Just for you to remember. You need to know where player is 
            //to know what you have to load around him

            int Quantity_of_chunks = Quantity_of_chunks_static;
            public World()
            {
                Initialiazing_World_as_Whole();
            }
            void Initialiazing_World_as_Whole()
            {
                for (int i = 0; i < Quantity_of_chunks; i++)
                {
                    World_as_Whole.Add(new List<Chunk>());
                    for (int j = 0; j < Quantity_of_chunks; j++)
                    {
                        World_as_Whole[i].Add(new Chunk());
                    }
                }

            }
        }

            //Кубик состоит из 6 граней, квадратов.
        }
}
