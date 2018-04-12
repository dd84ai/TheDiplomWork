using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class CubicalMemory : GeneralProgrammingStuff
    {
        //Flat Ground from height to height
        static int FromHeight = 8, ToHeight = 10;

        //World beginning
        //Логично будет начинатся в углу и в низу.
        static Point3D Point_of_static_beginning = new Point3D(0, 0, 0);

        //Player range of view
        //From 0(only current) to more than 0.
        static int Range_of_view_static = 2;

        static Random Rand = new Random();
        public class Cube
        {
            /// <summary>
            /// Размеры грани одного куба.
            /// </summary>
            public static float rangeOfTheEdge = 1.0f;
            public System.Drawing.Color color = ColorSwitch(Rand.Next(10));

            public bool IsFilled = false;
            public int x, y, z;
            public Cube(int _x, int _y, int _z)
            {
                x = _x; y = _y; z = _z;
            }
        }
        public class Chunk
        {
            /// <summary>
            /// Кол-во кубов в чанке.
            /// </summary>
            public static int Height = 32, Width = 16, Length = 16;

            public List<List<List<Cube>>> cubes;
            public int x, z;
            public Chunk(int _x, int _z)
            {
                x = _x; z = _z;
                cubes = TripleCubeIniter(Chunk.Width, Chunk.Length, Chunk.Height);

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
                for (int k = fromHeight; k >= 0 && k < toHeight && k < Height; k++)
                    for (int i = 0; i < Width; i++)
                        for (int j = 0; j < Length; j++)
                        {
                            cubes[i][k][j].IsFilled = true;
                            cubes[i][k][j].color = ColorSwitch(Rand.Next(10));
                        }
            }
        }



        public class World
        {
            public Point3D Point_of_beginning = new Point3D(Point_of_static_beginning);

            public int Range_of_view = Range_of_view_static;

            /// <summary>
            /// Кол-во чанков в мире.
            /// </summary>
            public static int Quantity_of_chunks_in_root = 6;

            public List<List<Chunk>> World_as_Whole = new List<List<Chunk>>();
            
            //Just for you to remember. You need to know where player is 
            //to know what you have to load around him

            public World()
            {
                Initialiazing_World_as_Whole();
            }
            void Initialiazing_World_as_Whole()
            {
                World_as_Whole = DoubleChunkIniter(Quantity_of_chunks_in_root, Quantity_of_chunks_in_root);
            }
        }
        public World world = new World();

        //Кубик состоит из 6 граней, квадратов.
    }
}
