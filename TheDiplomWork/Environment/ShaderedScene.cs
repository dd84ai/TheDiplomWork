using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
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
        class Quads
        {
            public void Add_Point(float _x, float _y, float _z)
            {

            }
            public static void Draw_Quad_Perpendecular_to_OSx(ref List<float> vertices,
                float start_y, float start_z,
               float end_y, float end_z,
               float height, OpenGL gl)
            {
                vertices.Add(height);
                vertices.Add(start_y);
                vertices.Add(start_z);

                vertices.Add(height);
                vertices.Add(start_y);
                vertices.Add(end_z);

                vertices.Add(height);
                vertices.Add(end_y);
                vertices.Add(end_z);

                vertices.Add(height);
                vertices.Add(end_y);
                vertices.Add(start_z);
            }
            //Right
            public static void Draw_Quad_Perpendecular_to_OSy(ref List<float> vertices,
                float start_x, float start_z,
               float end_x, float end_z,
               float height, OpenGL gl)
            {
                vertices.Add(start_x);
                vertices.Add(height);
                vertices.Add(start_z);

                vertices.Add(start_x);
                vertices.Add(height);
                vertices.Add(end_z);

                vertices.Add(end_x);
                vertices.Add(height);
                vertices.Add(end_z);

                vertices.Add(end_x);
                vertices.Add(height);
                vertices.Add(start_z);
            }
            public static void Draw_Quad_Perpendecular_to_OSz(ref List<float> vertices,
                float start_x, float start_y,
               float end_x, float end_y,
               float height, OpenGL gl)
            {
                vertices.Add(start_x);
                vertices.Add(start_y);
                vertices.Add(height);

                vertices.Add(end_x);
                vertices.Add(start_y);
                vertices.Add(height);

                vertices.Add(end_x);
                vertices.Add(end_y);
                vertices.Add(height);

                vertices.Add(start_x);
                vertices.Add(end_y);
                vertices.Add(height);
            }
            public static void Draw_Quad_Full(ref List<float> vertices,
                float x, float y, float z, float localed_range, OpenGL gl = null, SharpGL.Enumerations.BeginMode mod = 0)
            {
                if (gl != null)
                {
                    gl.Begin(mod);
                }

                //Front
                Quads.Draw_Quad_Perpendecular_to_OSz
                (ref vertices, //Where_to_write
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z, //Height
                gl);

                //Back
                Quads.Draw_Quad_Perpendecular_to_OSz
                (ref vertices, //Where_to_write
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z + localed_range, //Height
                gl //Height
                );

                //Left
                Quads.Draw_Quad_Perpendecular_to_OSx
                (ref vertices, //Where_to_write
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x, //Height
                gl //Height
                );

                //Right
                Quads.Draw_Quad_Perpendecular_to_OSx
                (ref vertices, //Where_to_write
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x + localed_range, //Height
                gl //Height
                );

                //Top
                Quads.Draw_Quad_Perpendecular_to_OSy
                (ref vertices, //Where_to_write
                x, //Start_x
                z, //Start_z
                x + localed_range, //End_x
                z + localed_range, //End_z
                y + localed_range, //Height
                gl //Height
                );

                //Bottom
                Quads.Draw_Quad_Perpendecular_to_OSy
                (ref vertices, //Where_to_write
                x, //Start_x
                z, //Start_z
                x + localed_range, //End_x
                z + localed_range, //End_z
                y, //Height
                gl //Height
                );

                if (gl != null)
                {
                    gl.End();
                }
            }
        }
        
        public void Initialization()
        {
            Memory_Init();
            float x = 0, y = 0, z = 0;

            //FOR OG WAR
            Quads.Draw_Quad_Perpendecular_to_OSy
                (ref vertices, //Where_to_write
                0, //Start_x
                0, //Start_z
                0 + CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Width * CubicalMemory.Cube.rangeOfTheEdge, //End_x
                0 + CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Length * CubicalMemory.Cube.rangeOfTheEdge, //End_z
                0, //Height
                null);

            for (int k = 0; k < 4; k++)
            {
                //XYZcube.color = GeneralProgrammingStuff.ColorSwitch(Rand.Next(10));
                colors.Add((float)System.Drawing.Color.Gray.R / 255);
                colors.Add((float)System.Drawing.Color.Gray.G / 255);
                colors.Add((float)System.Drawing.Color.Gray.B / 255);
            }


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

                                if (XYZcube.IsFilled)
                                {
                                    CalculateFromMaptoGraphical(XYworld, XYZcube, ref x, ref y, ref z);

                                    Quads.Draw_Quad_Full(ref vertices, x, y, z, localed_range);

                                    for (int k = 0; k < 4 * 6; k++)
                                    {
                                        //XYZcube.color = GeneralProgrammingStuff.ColorSwitch(Rand.Next(10));
                                        colors.Add((float)XYZcube.color.R / 255);
                                        colors.Add((float)XYZcube.color.G / 255);
                                        colors.Add((float)XYZcube.color.B / 255);
                                    }
                                }
                            }
                }

            CopiedLastResult = false;
        }
        public void CalculateFromMaptoGraphical(CubicalMemory.Chunk XYworld, CubicalMemory.Cube XYZcube, ref float x, ref float y, ref float z)
        {
            x = XYworld.x * CubicalMemory.Chunk.Width + XYZcube.x;
            y = XYZcube.y;
            z = XYworld.z * CubicalMemory.Chunk.Length + XYZcube.z;

            x *= (CubicalMemory.Cube.rangeOfTheEdge);
            y *= (CubicalMemory.Cube.rangeOfTheEdge);
            z *= (CubicalMemory.Cube.rangeOfTheEdge);
        }
        public bool FirstInitialization = false;
        public bool CopiedLastResult = false;

        int LastCount = 0;
        public int Quantity()
        {
            return LastCount;
        }
        public void CopyToReady()
        {
            Quantity_of_all_values = vertices.Count();
            FirstInitialization = true;
            CopiedLastResult = true;
            LastCount = vertices.Count();
        }
    }
}
