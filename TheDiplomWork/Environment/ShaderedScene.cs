using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
namespace TheDiplomWork
{
    public class ShaderedScene
    {
        public Environment env = new Environment();

        public static List<float> vertices = new List<float>();
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
        public class Quads
        {
            public static void Draw_Quad_Perpendecular_to_OSx(
                float start_y, float start_z,
               float end_y, float end_z,
               float height, OpenGL gl)
            {
                Process_Point(height, start_y, start_z, gl);
                Process_Point(height, start_y, end_z, gl);
                Process_Point(height, end_y, end_z, gl);
                Process_Point(height, end_y, start_z, gl);
            }
            //Right
            public static void Draw_Quad_Perpendecular_to_OSy(
                float start_x, float start_z,
               float end_x, float end_z,
               float height, OpenGL gl)
            {
                Process_Point(start_x, height, start_z, gl);
                Process_Point(start_x, height, end_z, gl);
                Process_Point(end_x, height, end_z, gl);
                Process_Point(end_x, height, start_z, gl);
            }
            public static void Draw_Quad_Perpendecular_to_OSz(
                float start_x, float start_y,
               float end_x, float end_y,
               float height, OpenGL gl)
            {
                Process_Point(start_x, start_y, height, gl);
                Process_Point(end_x, start_y, height, gl);
                Process_Point(end_x, end_y, height, gl);
                Process_Point(start_x, end_y, height, gl);
            }
            static void Process_Point(float _x, float _y, float _z, OpenGL gl)
            {
                if (gl != null)
                {
                    gl.Vertex(_x, _y, _z);
                }
                else
                {
                    vertices.Add(_x);
                    vertices.Add(_y);
                    vertices.Add(_z);
                }
            }
            public static void Draw_Quad_Full(
                float x, float y, float z, float localed_range, OpenGL gl = null, uint GLmod = 0)
            {
                if (gl != null)
                {
                    
                    gl.Color(0.0f, 0.0f, 0.0f);
                    gl.LoadIdentity();
                    gl.Begin(GLmod);
                }

                //Front
                Quads.Draw_Quad_Perpendecular_to_OSz
                (
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z, //Height
                gl);

                //Back
                Quads.Draw_Quad_Perpendecular_to_OSz
                (
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z + localed_range, //Height
                gl //Height
                );

                //Left
                Quads.Draw_Quad_Perpendecular_to_OSx
                (
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x, //Height
                gl //Height
                );

                //Right
                Quads.Draw_Quad_Perpendecular_to_OSx
                (
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x + localed_range, //Height
                gl //Height
                );

                //Top
                Quads.Draw_Quad_Perpendecular_to_OSy
                (
                x, //Start_x
                z, //Start_z
                x + localed_range, //End_x
                z + localed_range, //End_z
                y + localed_range, //Height
                gl //Height
                );

                //Bottom
                Quads.Draw_Quad_Perpendecular_to_OSy
                (
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
        vec4 CalculatingThing = new vec4(0, 0, 0, 0);
        public void OpenGLDraw(OpenGL gl,mat4 matrix_all_inclusive)
        {
            float x = 0, y = 0, z = 0;
            CalculateFromMaptoGraphical(Scene.SS.env.player.coords.Player_chunk_lookforcube,
                Scene.SS.env.player.coords.Player_cubical_lookforcube, ref x, ref y, ref z);

            CalculatingThing.x = x;
            CalculatingThing.y = y;
            CalculatingThing.z = z;
            CalculatingThing.w = 0;
            //CalculatingThing = new vec4(0, 0, 0, 0);

            CalculatingThing = matrix_all_inclusive * CalculatingThing;

            x = CalculatingThing.x;
            y = CalculatingThing.y;
            z = CalculatingThing.z;

            Quads.Draw_Quad_Full(x, y, z, localed_range,gl,OpenGL.GL_QUADS);
        }
        
        float localed_range = CubicalMemory.Cube.rangeOfTheEdge * 9 / 10;
        public void Initialization()
        {
            Memory_Init();
            float x = 0, y = 0, z = 0;
            //FOR OG WAR
            Quads.Draw_Quad_Perpendecular_to_OSy
                (
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

            foreach (var Xworld in env.cub_mem.world.World_as_Whole)
                foreach (var XYworld in Xworld)
                {
                    if (Math.Abs(Scene.SS.env.player.coords.Player_chunk_position.x - XYworld.xz.x) < Scene.SS.env.player.coords.RangeOfView &&
                        Math.Abs(Scene.SS.env.player.coords.Player_chunk_position.z - XYworld.xz.z) < Scene.SS.env.player.coords.RangeOfView)
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
                                    CalculateFromMaptoGraphical(XYworld.xz, XYZcube.xyz, ref x, ref y, ref z);

                                    Quads.Draw_Quad_Full(x, y, z, localed_range);

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
        public void CalculateFromMaptoGraphical(GeneralProgrammingStuff.Point2Int XYworld, GeneralProgrammingStuff.Point3Int XYZcube, ref float x, ref float y, ref float z)
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
