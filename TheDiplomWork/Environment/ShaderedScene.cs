using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheDiplomWork
{
    public class ShaderedScene
    {
        Environment env = new Environment();

        public float[] vertices = null;
        public float[] colors = null;
        public ShaderedScene()
        {
            Initialization();
        }
        public int Quantity_of_total_cubes = 0;
        public int Quantity_of_values_per_point = 3;
        public int Quantity_of_points_per_side = 4;
        public int Quantity_of_sides_per_cube = 6;
        public int Quantity_of_cubes_per_chunk = 0;

        public int Quantity_of_all_points= 0;
        public int Quantity_of_all_values = 0;
        void Memory_Init()
        {
            Quantity_of_cubes_per_chunk =
                CubicalMemory.Chunk.Height
                * CubicalMemory.Chunk.Length
                * CubicalMemory.Chunk.Width;

            Quantity_of_total_cubes = CubicalMemory.World.Quantity_of_chunks_in_root
                * CubicalMemory.World.Quantity_of_chunks_in_root
                * Quantity_of_cubes_per_chunk;



            Quantity_of_all_values =
                Quantity_of_total_cubes * Quantity_of_values_per_point
                * Quantity_of_points_per_side * Quantity_of_sides_per_cube;

            Quantity_of_all_points =
                Quantity_of_total_cubes
                * Quantity_of_points_per_side * Quantity_of_sides_per_cube;

            vertices = new float[Quantity_of_all_values];
            colors = new float[Quantity_of_all_values];
        }
        void Initialization()
        {
            Memory_Init();

            List<float> listed_vertices = new List<float>();
            List<float> listed_colors = new List<float>();
            int counter = 0;

            foreach (var Xworld in env.cub_mem.world.World_as_Whole)
                foreach (var XYworld in Xworld)
                {
                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                if (counter % 1000 == 0)
                                    Console.WriteLine($"{counter}\\{Quantity_of_total_cubes}");

                                counter++;
                                //int Chunk_number =
                                //    XYworld.y * CubicalMemory.World.Quantity_of_chunks_in_root
                                //    + XYworld.x;
                                //int Cube_number = Chunk_number
                                //* Quantity_of_cubes_per_chunk +
                                //    (CubicalMemory.Chunk.Width * CubicalMemory.Chunk.Length
                                //    * (XYZcube.z))
                                //    + (CubicalMemory.Chunk.Width
                                //    * (XYZcube.y))
                                //    + XYZcube.x;

                                //if (vertices[cube_number * 3 + 0] == 1
                                //    || vertices[cube_number * 3 + 1] == 1
                                //    || vertices[cube_number * 3 + 2] == 1)
                                //{
                                //    console.writeline("error");
                                //    console.readkey();
                                //}

                                //vertices[Cube_number * 3 + 0] = 1;
                                //vertices[Cube_number * 3 + 1] = 1;
                                //vertices[Cube_number * 3 + 2] = 1;

                                if (XYZcube.IsFilled)
                                {
                                    float x = XYworld.x * CubicalMemory.Chunk.Width + XYZcube.x;
                                    float y = XYZcube.y;
                                    float z = XYworld.z * CubicalMemory.Chunk.Length + XYZcube.z;

                                    x *= CubicalMemory.Cube.rangeOfTheEdge;
                                    y *= CubicalMemory.Cube.rangeOfTheEdge;
                                    z *= CubicalMemory.Cube.rangeOfTheEdge;

                                    //Front
                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);
                                    //Back
                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);
                                    //Left
                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);
                                    //Right
                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);
                                    //Top
                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y + 1);
                                    listed_vertices.Add(z + 1);
                                    //Bottom
                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z);

                                    listed_vertices.Add(x + 1);
                                    listed_vertices.Add(y);
                                    listed_vertices.Add(z + 1);

                                    for (int i = 0; i < 6 * 4; i++)
                                    {
                                        listed_colors.Add((float)XYZcube.color.R / 255);
                                        listed_colors.Add((float)XYZcube.color.G / 255);
                                        listed_colors.Add((float)XYZcube.color.B / 255);
                                    }
                                }
                            }
                }

            //foreach (var item in vertices)
            //    if (item != 1)
            //    {
            //        Console.WriteLine("Error");
            //        Console.ReadKey();
            //    }
            //Console.WriteLine("Initialization");

            vertices = listed_vertices.ToArray();
            colors = listed_colors.ToArray();
        }
    }
}
