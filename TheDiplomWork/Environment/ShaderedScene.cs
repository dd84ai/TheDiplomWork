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

        public float[] vertices = null;
        public float[] colors = null;
        public float[] prepared_vertices = null;
        public float[] prepared_colors = null;
        public float[] third_vertices = null;
        public float[] third_colors = null;
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

            int Capacity = 1000000;
            vertices = new float[Capacity];
            colors = new float[Capacity];
            prepared_vertices = vertices;//new float[Capacity];
            prepared_colors = colors;//new float[Capacity];
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

            counter = 0;
            float localed_range = CubicalMemory.Cube.rangeOfTheEdge * 9 / 10;

            foreach (var Xworld in env.cub_mem.world.World_as_Whole)
                foreach (var XYworld in Xworld)
                {
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

                                    int start = counter;
                                    //Front
                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;
                                    //Back
                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    //Left
                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;

                                    //Right
                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;

                                    //Top
                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y + localed_range ;
                                    prepared_vertices[counter++]=z ;

                                    //Bottom
                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    prepared_vertices[counter++]=x ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;


                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z + localed_range ;

                                    prepared_vertices[counter++]=x + localed_range ;
                                    prepared_vertices[counter++]=y ;
                                    prepared_vertices[counter++]=z ;

                                    counter = start;
                                    for (int i = 0; i < 4 * 6; i++)
                                    {
                                        XYZcube.color = GeneralProgrammingStuff.ColorSwitch(Rand.Next(10));
                                        prepared_colors[counter++] = (float)XYZcube.color.R / 255;
                                        prepared_colors[counter++] = (float)XYZcube.color.G / 255;
                                        prepared_colors[counter++] = (float)XYZcube.color.B / 255;
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
        public void CopyToReady()
        {
            
                third_vertices = vertices;
                third_colors = colors;
                vertices = prepared_vertices;
                colors = prepared_colors;
                prepared_vertices = third_vertices;
                prepared_colors = third_colors;
                Quantity_of_all_values = counter;
                FirstInitialization = true;
            CopiedLastResult = true;

        }
    }
}
