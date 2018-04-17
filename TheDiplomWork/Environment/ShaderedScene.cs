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

        public ShaderedScene()
        {
            //Initialization();
            //CopyToReady();
        }
        


        Random Rand = new Random();

        public class DataForDraw
        {
            public int Quantity_of_total_cubes = 0;
            public int Quantity_of_values_per_point = 3;
            public int Quantity_of_points_per_side = 4;
            public int Quantity_of_sides_per_cube = 6;
            public int Quantity_of_cubes_per_chunk = 0;

            public int Quantity_of_all_points = 0;
            public int Quantity_of_all_values = 0;
            public void Memory_Init()
            {

                //vertices.Clear();
                //colours.Clear();
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

            public List<float> vertices = new List<float>();
            public List<float> colours = new List<float>();
            public int vertices_count = 0;
            public int colours_count = 0;

            public void Draw_Quad_Perpendecular_to_OSx(
                float start_y, float start_z,
               float end_y, float end_z,
               float height, System.Drawing.Color _colour)
            {
                Process_Point(height, start_y, start_z, _colour);
                Process_Point(height, start_y, end_z, _colour);
                Process_Point(height, end_y, end_z, _colour);
                Process_Point(height, end_y, start_z, _colour);
            }
            //Right
            public void Draw_Quad_Perpendecular_to_OSy(
                float start_x, float start_z,
               float end_x, float end_z,
               float height, System.Drawing.Color _colour)
            {
                Process_Point(start_x, height, start_z, _colour);
                Process_Point(start_x, height, end_z, _colour);
                Process_Point(end_x, height, end_z, _colour);
                Process_Point(end_x, height, start_z, _colour);
            }
            public void Draw_Quad_Perpendecular_to_OSz(
                float start_x, float start_y,
               float end_x, float end_y,
               float height, System.Drawing.Color _colour)
            {
                Process_Point( start_x, start_y, height, _colour);
                Process_Point(end_x, start_y, height, _colour);
                Process_Point(end_x, end_y, height, _colour);
                Process_Point(start_x, end_y, height, _colour);
            }
            void Process_Point(float x, float y, float z, System.Drawing.Color _colour)
            {
                Add_Value(ref vertices, vertices_count++, x);
                Add_Value(ref vertices, vertices_count++, y);
                Add_Value(ref vertices, vertices_count++, z);

                Add_Value(ref colours, colours_count++, (float)_colour.R / 255);
                Add_Value(ref colours, colours_count++, (float)_colour.G / 255);
                Add_Value(ref colours, colours_count++, (float)_colour.B / 255);
            }
            public void Draw_Quad_Full_Sunsided(
                float x, float y, float z, float localed_range, System.Drawing.Color _colour, bool SunSided = false)
            {
                //Front
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(0, 0, -1)) < StaticSettings.S.SunSidedCoef)
                Draw_Quad_Perpendecular_to_OSz
                (
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z, //Height
                _colour);


                //Back
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(0, 0, 1)) < StaticSettings.S.SunSidedCoef)
                    Draw_Quad_Perpendecular_to_OSz
                (
                x, //Start_x
                y, //Start_z
                x + localed_range, //End_x
                y + localed_range, //End_z
                z + localed_range, //Height
                _colour //Height
                );

                //Left
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(-1, 0, 0)) < StaticSettings.S.SunSidedCoef)
                    Draw_Quad_Perpendecular_to_OSx
                (
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x, //Height
                _colour //Height
                );

                //Right
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(1, 0, 0)) < StaticSettings.S.SunSidedCoef)
                    Draw_Quad_Perpendecular_to_OSx
                (
                y, //Start_x
                z, //Start_z
                y + localed_range, //End_x
                z + localed_range, //End_z
                x + localed_range, //Height
                _colour //Height
                );

                //Top
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(0, 1, 0)) < StaticSettings.S.SunSidedCoef)
                    Draw_Quad_Perpendecular_to_OSy
                (
                x, //Start_x
                z, //Start_z
                x + localed_range, //End_x
                z + localed_range, //End_z
                y + localed_range, //Height
                _colour //Height
                );

                //Bottom
                if (SunSided || -GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.NormalizedLook,
                    new vec3(0, -1, 0)) < StaticSettings.S.SunSidedCoef)
                    Draw_Quad_Perpendecular_to_OSy
                (
                x, //Start_x
                z, //Start_z
                x + localed_range, //End_x
                z + localed_range, //End_z
                y, //Height
                _colour //Height
                );
            }

            public void Extra_Remover(ref List<float> list, int max_count)
            {
                if (max_count < list.Count())
                    list.RemoveRange(max_count, list.Count() - 1 - max_count);
            }
            public float[] vertices_arrayed;
            public float[] colours_arrayed;
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
            void Add_Value(ref List<float> list, int index, float value)
            {
                if (index < list.Count()) list[index] = value;
                else list.Add(value);
            }
            public void GhostCubeInit()
            {
                Memory_Init();
                vertices_count = 0;
                colours_count = 0;

                bool found = false;
                try
                {
                    bool thing = Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled;
                    if (!thing) found = true;
                }
                catch (Exception e)
                {
                    found = false;
                }

                if (found)
                {
                    float x = 0, y = 0, z = 0;
                    CalculateFromMaptoGraphical(Scene.SS.env.player.coords.Player_chunk_lookforcube,
                        Scene.SS.env.player.coords.Player_cubical_lookforcube, ref x, ref y, ref z);

                    Draw_Quad_Full_Sunsided(x, y, z, localed_range, System.Drawing.Color.White, true);

                }
                Extra_Remover(ref vertices, vertices_count);
                Extra_Remover(ref colours, colours_count);
                CopiedLastResult = false;
                vertices_arrayed = vertices.ToArray();
                colours_arrayed = colours.ToArray();

                CopyToReady();
            }
        }
        
        static vec4 CalculatingThing = new vec4(0, 0, 0, 0);
        static mat4 matrix_all_inclusive;
        public void OpenGLDraw(OpenGL gl,mat4 _matrix_all_inclusive)
        {
            matrix_all_inclusive = _matrix_all_inclusive;
            CalculateFromMaptoGraphical(Scene.SS.env.player.coords.Player_chunk_lookforcube,
                Scene.SS.env.player.coords.Player_cubical_lookforcube, ref x, ref y, ref z);

            //CalculatingThing = new vec4(0, 0, 0, 0);
            x = 0; y = 0; z = 0;

            //Quads.Draw_Quad_Full(x, y, z, localed_range,gl,OpenGL.GL_QUADS);

        }

        float x = 0, y = 0, z = 0;
        static float localed_range = CubicalMemory.Cube.rangeOfTheEdge * 9 / 10;
        vec3 NormalizedToXYWorld = new vec3(0, 0, 0);
        public vec3 LastPlayerLook = new vec3(0, 0, 0);
        public DataForDraw Main = new DataForDraw();
        public DataForDraw GhostCube = new DataForDraw();
        
        public void Initialization()
        {
            Main.Memory_Init();
            Main.vertices_count = 0;
            Main.colours_count = 0;
            LastPlayerLook.x = Scene.SS.env.player.coords.NormalizedLook.x;
            LastPlayerLook.y = Scene.SS.env.player.coords.NormalizedLook.y;
            LastPlayerLook.z = Scene.SS.env.player.coords.NormalizedLook.z;

            //FOR OG WAR
            Main.Draw_Quad_Perpendecular_to_OSy
                (
                0, //Start_x
                0, //Start_z
                CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Width * CubicalMemory.Cube.rangeOfTheEdge, //End_x
                CubicalMemory.World.Quantity_of_chunks_in_root * CubicalMemory.Chunk.Length * CubicalMemory.Cube.rangeOfTheEdge, //End_z
                0, //Height
                System.Drawing.Color.Gray
                );

            int i = 0;
            int j = 0;

            int value = 0;
            if ((value = Scene.SS.env.player.coords.Player_chunk_position.x - StaticSettings.S.RangeOfView) > 0)
                i = value; else i = 0;

            for (; i < env.cub_mem.world.World_as_Whole.Count() && i < Scene.SS.env.player.coords.Player_chunk_position.x + StaticSettings.S.RangeOfView; i++)
            {
                if ((value = Scene.SS.env.player.coords.Player_chunk_position.z - StaticSettings.S.RangeOfView) > 0)
                    j = value; else j = 0;

            for (; j < env.cub_mem.world.World_as_Whole[i].Count() && j < Scene.SS.env.player.coords.Player_chunk_position.z + StaticSettings.S.RangeOfView; j++)
                {
                    var XYworld = env.cub_mem.world.World_as_Whole[i][j];

                    foreach (var Xcube in XYworld.cubes)
                        foreach (var XYcube in Xcube)
                            foreach (var XYZcube in XYcube)
                            {
                                if (XYZcube.IsFilled)
                                {
                                    CalculateFromMaptoGraphical(XYworld.xz, XYZcube.xyz, ref x, ref y, ref z);

                                    //POINT OF VIEWER
                                    NormalizedToXYWorld.x = Scene.SS.env.player.coords.Player_precise_stepback.x - x;
                                    NormalizedToXYWorld.y = Scene.SS.env.player.coords.Player_precise_stepback.y - y;
                                    NormalizedToXYWorld.z = Scene.SS.env.player.coords.Player_precise_stepback.z - z;
                                    float range = GeneralProgrammingStuff.vec3_range(NormalizedToXYWorld);
                                    GeneralProgrammingStuff.vec3_normalize(ref NormalizedToXYWorld, range);
                                    Scene.SS.env.player.coords.LookForCube();
                                    float scalar = GeneralProgrammingStuff.vec3_scalar(NormalizedToXYWorld, Scene.SS.env.player.coords.NormalizedLook);
                                    //POINT OF VIEWER

                                    if (!StaticSettings.S.RealoderCauseOfPointOfView || scalar > 0 && range < CubicalMemory.Cube.rangeOfTheEdge * CubicalMemory.Chunk.Width * StaticSettings.S.RangeOfView)
                                    {
                                        if (!StaticSettings.S.RealoderCauseOfSunSided ||
                                            XYworld.xz.x == Scene.SS.env.player.coords.Player_chunk_position.x
                                            && XYworld.xz.z == Scene.SS.env.player.coords.Player_chunk_position.z)
                                            Main.Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color, true);
                                        else
                                        Main.Draw_Quad_Full_Sunsided(x, y, z, localed_range, XYZcube.color);
                                    }
                                }
                            }
                }
            }
            Main.Extra_Remover(ref Main.vertices, Main.vertices_count);
            Main.Extra_Remover(ref Main.colours, Main.colours_count);
            Main.CopiedLastResult = false;
            Main.vertices_arrayed = Main.vertices.ToArray();
            Main.colours_arrayed = Main.colours.ToArray();
        }
        
        public static void CalculateFromMaptoGraphical(GeneralProgrammingStuff.Point2Int XYworld, GeneralProgrammingStuff.Point3Int XYZcube, ref float x, ref float y, ref float z)
        {
            x = XYworld.x * CubicalMemory.Chunk.Width + XYZcube.x;
            y = XYZcube.y;
            z = XYworld.z * CubicalMemory.Chunk.Length + XYZcube.z;

            x *= (CubicalMemory.Cube.rangeOfTheEdge);
            y *= (CubicalMemory.Cube.rangeOfTheEdge);
            z *= (CubicalMemory.Cube.rangeOfTheEdge);
        }
        
    }
}
