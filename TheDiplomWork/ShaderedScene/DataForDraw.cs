using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public partial class DataForDraw
    {
        protected static float localed_range = CubicalMemory.Cube.rangeOfTheEdge * 9 / 10;
        public int Quantity_of_total_cubes = 0;
        public int Quantity_of_values_per_point = 3;
        public int Quantity_of_points_per_side = 4;
        public int Quantity_of_sides_per_cube = 6;
        public int Quantity_of_cubes_per_chunk = 0;

        public int Quantity_of_all_points = 0;
        public int Quantity_of_all_values = 0;
        public void Memory_Init()
        {
            Initialization_in_process = true;
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
            Process_Point(start_x, start_y, height, _colour);
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
        public bool CopiedLastResult = true;

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
            Initialization_in_process = false;
        }
        void Add_Value(ref List<float> list, int index, float value)
        {
            if (index < list.Count()) list[index] = value;
            else list.Add(value);
        }
        protected float x = 0, y = 0, z = 0;

        public bool Initialization_in_process = false;
        public virtual void initialization()
        { }
        public void START_initialization()
        {
            Memory_Init();
            vertices_count = 0;
            colours_count = 0;
        }
        public void END_initialization()
        {
            Extra_Remover(ref vertices, vertices_count);
            Extra_Remover(ref colours, colours_count);
            CopiedLastResult = false;
            vertices_arrayed = vertices.ToArray();
            colours_arrayed = colours.ToArray();
        }
    }
}
