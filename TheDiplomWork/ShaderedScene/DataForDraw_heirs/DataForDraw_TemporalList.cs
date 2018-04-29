using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class DataForDraw_TemporalList : DataForDraw_angled
    {
        public class Chunk_and_Cube
        {
            public CubicalMemory.Chunk chunk;
            public CubicalMemory.Cube cube;
            public Chunk_and_Cube(CubicalMemory.Chunk a, CubicalMemory.Cube b)
            {
                chunk = a; cube = b;
            }
        }
        public static List<DataForDraw_TemporalList.Chunk_and_Cube> TemporalList = new List<DataForDraw_TemporalList.Chunk_and_Cube>();
        public override void initialization()
        {
            START_initialization();

            foreach (var item in TemporalList)
            {
                ShaderedScene.CalculateFromMaptoGraphical(item.chunk.xz, item.cube.xyz, ref x, ref y, ref z);
                Draw_Quad_Full_Sunsided_angled(x, y, z, 0.0f, item.cube.FallingFromHeight * (CubicalMemory.Cube.rangeOfTheEdge), 0.0f, localed_range, item.cube.color, 0, true);
            }

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
