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

        protected float[] vertices = null;
        protected float[] colors = null;
        public ShaderedScene()
        {
            
        }
        public static int Quantity_of_cubes = 0;
        public static int Quantity_of_values_per_point = 3;
        public static int Quantity_of_points_per_side = 4;
        public static int Quantity_of_sides_per_cube = 6;

        public static int Quantity_of_all_points= 0;
        public static int Quantity_of_all_values = 0;
        void Memory_Init()
        {
            Quantity_of_cubes = CubicalMemory.World.Quantity_of_chunks
                * CubicalMemory.Chunk.Height
                * CubicalMemory.Chunk.Length
                * CubicalMemory.Chunk.Width;

            Quantity_of_all_values = 
                Quantity_of_cubes * Quantity_of_values_per_point
                * Quantity_of_points_per_side * Quantity_of_sides_per_cube;

            Quantity_of_all_points =
                Quantity_of_cubes
                * Quantity_of_points_per_side * Quantity_of_sides_per_cube;

            vertices = new float[Quantity_of_all_values];
            colors = new float[Quantity_of_all_values];
        }
        void Initialization()
        {
            Memory_Init();


            for (int i = 0; i < CubicalMemory.World.Quantity_of_chunks; i++)
                for (int j = 0; j < CubicalMemory.World.Quantity_of_chunks; j++)
                {
                    for (int x = 0; x < CubicalMemory.Chunk.Width; x++)
                        for (int y = 0; y < CubicalMemory.Chunk.Length; y++)
                            for (int z = 0; z < CubicalMemory.Chunk.Height; z++)
                            {
                                
                            }
                }
                    
        }
    }
}
