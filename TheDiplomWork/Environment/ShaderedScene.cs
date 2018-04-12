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

            foreach (var Xworld in env.cub_mem.world.World_as_Whole)
                foreach (var XYworld in Xworld)
                {
                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                int Chunk_number =
                                    XYworld.y * CubicalMemory.World.Quantity_of_chunks_in_root
                                    + XYworld.x;
                                int Cube_number = Chunk_number
                                * Quantity_of_cubes_per_chunk +
                                    (CubicalMemory.Chunk.Width * CubicalMemory.Chunk.Length
                                    * (XYZcube.z))
                                    + (CubicalMemory.Chunk.Width
                                    * (XYZcube.y))
                                    + XYZcube.x;

                                if (vertices[Cube_number * 3 + 0] == 1
                                    || vertices[Cube_number * 3 + 1] == 1
                                    || vertices[Cube_number * 3 + 2] == 1)
                                {
                                    Console.WriteLine("Error");
                                    Console.ReadKey();
                                }
                                vertices[Cube_number * 3 + 0] = 1;
                                vertices[Cube_number * 3 + 1] = 1;
                                vertices[Cube_number * 3 + 2] = 1;
                            }
                }

            foreach (var item in vertices)
                if (item != 1)
                {
                    Console.WriteLine("Error");
                    Console.ReadKey();
                }
            Console.WriteLine("Initialization");
        }
    }
}
