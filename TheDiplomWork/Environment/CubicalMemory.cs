using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class CubicalMemory : GeneralProgrammingStuff
    {
        static Random Rand = new Random(Seed);
        public CubicalMemory()
        {
            Rand = new Random(Seed);
            world = new World();
    }
        
        public static int Seed = 46;
        //Flat Ground from height to height
        static int FromHeight = 8, ToHeight = 10;

        //World beginning
        //Логично будет начинатся в углу и в низу.
        static Point3D Point_of_static_beginning = new Point3D(0, 0, 0);

        public class Cube
        {
            /// <summary>
            /// Размеры грани одного куба.
            /// </summary>
            public static float rangeOfTheEdge = 1.0f;
            public System.Drawing.Color color = ColorSwitch(Rand.Next(10));
            //public bool Changed = false;
            public bool IsFilled = false;
            public GeneralProgrammingStuff.Point3Int xyz = new Point3Int(0,0,0);

            public bool IsFilled_Default;
            public System.Drawing.Color color_default;
            public Cube(int _x, int _y, int _z)
            {
                xyz.x = _x; xyz.y = _y; xyz.z = _z;
            }
        }
        public class Chunk
        {
            /// <summary>
            /// Кол-во кубов в чанке.
            /// </summary>
            public static int Height = 32, Width = 16, Length = 16;
            public List<List<List<Cube>>> cubes;
            public GeneralProgrammingStuff.Point2Int xz = new Point2Int(0,0);
            public Chunk(int _x, int _z)
            {
                xz.x = _x; xz.z = _z;
                cubes = TripleCubeIniter(Chunk.Width, Chunk.Height, Chunk.Length);

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
                            cubes[i][k][j].color = ColorSwitch(Rand.Next(11));
                        }
            }
        }



        public class World
        {
            public Point3D Point_of_beginning = new Point3D(Point_of_static_beginning);
            
            /// <summary>
            /// Кол-во чанков в мире.
            /// </summary>
            public static int Quantity_of_chunks_in_root = 16;

            public List<List<Chunk>> World_as_Whole = new List<List<Chunk>>();
            
            //Just for you to remember. You need to know where player is 
            //to know what you have to load around him

            public World()
            {
                Initialiazing_World_as_Whole();
                Default_State();
            }
            void Initialiazing_World_as_Whole()
            {
                World_as_Whole = DoubleChunkIniter(Quantity_of_chunks_in_root, Quantity_of_chunks_in_root);
            }

            void Default_State()
            {
                foreach (var Xworld in World_as_Whole)
                    foreach (var XYworld in Xworld)
                    {

                        foreach (var Xcube in XYworld.cubes)
                            foreach (var XYcube in Xcube)
                                foreach (var XYZcube in XYcube)
                                {
                                    XYZcube.color_default = XYZcube.color;
                                    XYZcube.IsFilled_Default = XYZcube.IsFilled;
                                }
                    }
            }
        }
        public World world;

        //Кубик состоит из 6 граней, квадратов.
    }
}
