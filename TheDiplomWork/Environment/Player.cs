using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public class Player : GeneralProgrammingStuff
    {
        public class Coords
        {
            //По идеи данная стартовая позиция будет ровно посередине мира и немного выше.
            public Point3D Player_precise_position = new Point3D(0, 0, 0);

            //public Point3D Player_Default_position = new Point3D(0, 0, 0);
            public Point3D Player_Default_position = new Point3D(
                (float)(((8 + 8 * CubicalMemory.Chunk.Width) * (double)CubicalMemory.Cube.rangeOfTheEdge)),
                11.5f * CubicalMemory.Cube.rangeOfTheEdge,
                (float)(((8 + 8 * CubicalMemory.Chunk.Length) * (double)CubicalMemory.Cube.rangeOfTheEdge)));
            public Point3D Player_rotational_view = new Point3D(3.14f / 2f, 0, 0);

            public Point2Int Player_chunk_position = new Point2Int(0,0);
            public Point2Int Player_chunk_position_OLD = new Point2Int(0, 0);

            public Point2Int Player_cubical_position = new Point2Int(0,0);

            public bool RangeReloader = true;
            public int RangeOfView = 4;

            public void Player_recalculate_extra_positions()
            {
                Reverse_presice_to_map_coords(Player_precise_position, ref Player_chunk_position, ref Player_cubical_position);
                LookForCube();
            }
            /// <summary>
            /// Recalculate coordinates from player graphic position to chunk & cubical position.
            /// </summary>
            public void Reverse_presice_to_map_coords(Point3D _precise, ref Point2Int _chunk, ref Point2Int _cubical)
            {
                _chunk.x = (int)((_precise.x / (double)CubicalMemory.Cube.rangeOfTheEdge) / (double)CubicalMemory.Chunk.Width);
                _chunk.z = (int)((_precise.z / (double)CubicalMemory.Cube.rangeOfTheEdge) / (double)CubicalMemory.Chunk.Length);

                _cubical.x = (int)(_precise.x / (double)CubicalMemory.Cube.rangeOfTheEdge);
                _cubical.z = (int)(_precise.z / (double)CubicalMemory.Cube.rangeOfTheEdge);

                _cubical.x -= _chunk.x * CubicalMemory.Chunk.Width;
                _cubical.z -= _chunk.z * CubicalMemory.Chunk.Length;
            }

            public Point3D Player_precise_lookforcube = new Point3D(0, 0, 0);
            public Point2Int Player_chunk_lookforcube = new Point2Int(0, 0);
            public Point2Int Player_cubical_lookforcube = new Point2Int(0, 0);
            vec4 StepForARequiedCube = new vec4(0,0, 0.25f*CubicalMemory.Cube.rangeOfTheEdge*4,0);
            
            void LookForCube()
            {
                GeneralProgrammingStuff.Point3D.CopyToFrom(
                    ref Player_precise_lookforcube, Player_precise_position);
                Keyboard.Wrapped_Do_Step(StepForARequiedCube, ref Player_precise_lookforcube);
                Reverse_presice_to_map_coords(Player_precise_lookforcube, ref Player_chunk_lookforcube, ref Player_cubical_lookforcube);
            }
            /// <summary>
            /// Short link to Player_recalculate_extra_positions.
            /// </summary>
            void Player_rec() { Player_recalculate_extra_positions(); }
        }
        public Coords coords = new Coords();


    }
}
