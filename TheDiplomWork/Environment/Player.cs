using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class Player : GeneralProgrammingStuff
    {
        public class Coords
        {
            //По идеи данная стартовая позиция будет ровно посередине мира и немного выше.
            public Point3D Player_precise_position = new Point3D(0, 0, -CubicalMemory.Cube.rangeOfTheEdge*CubicalMemory.Chunk.Length*CubicalMemory.World.Quantity_of_chunks_in_root* Environment.SizeView);
            public Point3D Player_rotational_view = new Point3D(0, 0, 0);

            public Point2Int Player_chunk_position = new Point2Int(0,0);

            public Point2Int Player_cubical_position = new Point2Int(0,0);

            /// <summary>
            /// Recalculate coordinates from player graphic position to chunk & cubical position.
            /// </summary>
            void Player_recalculate_extra_positions()
            {
                Player_chunk_position.x = (int)(Player_precise_position.x / (double)CubicalMemory.Chunk.Width);
                Player_chunk_position.z = (int)(Player_precise_position.z / (double)CubicalMemory.Chunk.Length);

                Player_cubical_position.x = (int)(Player_precise_position.x / (double)CubicalMemory.Cube.rangeOfTheEdge);
                Player_cubical_position.z = (int)(Player_precise_position.z / (double)CubicalMemory.Cube.rangeOfTheEdge);
                Player_cubical_position.z = (int)(Player_precise_position.y / (double)CubicalMemory.Cube.rangeOfTheEdge);
            }
            /// <summary>
            /// Short link to Player_recalculate_extra_positions.
            /// </summary>
            void Player_rec() { Player_recalculate_extra_positions(); }
        }
        public Coords coords = new Coords();


    }
}
