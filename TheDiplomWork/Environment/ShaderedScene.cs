using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheDiplomWork
{
    public class ShaderedScene
    {
        public Environment env = new Environment();

        public List<float> vertices = new List<float>();
        public List<float> colors = new List<float>();
        public ShaderedScene()
        {
            //Initialization();
            //CopyToReady();
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
            vertices.Clear();
            colors.Clear();

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
        }

        Random Rand = new Random();
        int counter = 0;
        public void plus()
        {
            counter++;
        }
        public void Initialization()
        {
            Memory_Init();

            float localed_range = CubicalMemory.Cube.rangeOfTheEdge * 9 / 10;

            foreach (var Xworld in env.cub_mem.world.World_as_Whole)
                foreach (var XYworld in Xworld)
                {
                    if (Math.Abs(Scene.SS.env.player.coords.Player_chunk_position.x - XYworld.x) < Scene.SS.env.player.coords.RangeOfView &&
                        Math.Abs(Scene.SS.env.player.coords.Player_chunk_position.z - XYworld.z) < Scene.SS.env.player.coords.RangeOfView)
                    { }
                    else continue;

                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                //if (counter % 1000 == 0)
                                //    Console.WriteLine($"{counter}\\{Quantity_of_total_cubes}");

                                
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

                                    x *= (CubicalMemory.Cube.rangeOfTheEdge);
                                    y *= (CubicalMemory.Cube.rangeOfTheEdge);
                                    z *= (CubicalMemory.Cube.rangeOfTheEdge);

                                    //Front
                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );

                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );
                                    //Back
                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );

                                    //Left
                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z );

                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );

                                    //Right
                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );

                                    //Top
                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );

                                    vertices.Add(x );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y + localed_range );
                                    vertices.Add(z );

                                    //Bottom
                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z );

                                    vertices.Add(x );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );


                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z + localed_range );

                                    vertices.Add(x + localed_range );
                                    vertices.Add(y );
                                    vertices.Add(z );

  
                                    for (int k = 0; k < 4 * 6; k++)
                                    {
                                        //XYZcube.color = GeneralProgrammingStuff.ColorSwitch(Rand.Next(10));
                                        colors.Add((float)XYZcube.color.R / 255);
                                        colors.Add((float)XYZcube.color.G / 255);
                                        colors.Add((float)XYZcube.color.B / 255);
                                    }
                                    /*for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)0 / 255);
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)0 / 255);
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)255 / 255);
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)0 / 255);
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)255 / 255);
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        listed_colors.Add((float)0 / 255);
                                        listed_colors.Add((float)255 / 255);
                                        listed_colors.Add((float)255 / 255);
                                    }*/
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

            CopiedLastResult = false;
        }
        
        public bool FirstInitialization = false;
        public bool CopiedLastResult = false;
        public int Quantity()
        {
            return vertices.Count();
        }
        public void CopyToReady()
        {
            Quantity_of_all_values = vertices.Count();
            FirstInitialization = true;
            CopiedLastResult = true;

        }
    }
}
