using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_TemporalList : DataForDraw_angled
    {
        public DataForDraw_TemporalList(OpenGL gl) : base(gl)
        {
        }
        public static List<CubicalMemory.Chunk_and_Cube_link> TemporalList = new List<CubicalMemory.Chunk_and_Cube_link>();
        public override void initialization()
        {
            START_initialization();

            foreach (var item in TemporalList)
            {
                ShaderedScene.CalculateFromMaptoGraphical(item.cube, ref x, ref y, ref z);
                Draw_Quad_Full_Sunsided_angled(x, y, z, item.cube.FallingStartingTime, item.cube.FallingFromHeight * (CubicalMemory.Cube.rangeOfTheEdge), 0.0f, localed_range, item.cube.color, 0, true);
            }

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
