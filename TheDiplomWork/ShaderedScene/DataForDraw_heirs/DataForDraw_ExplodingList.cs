using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class DataForDraw_ExplodingList : DataForDraw_angled
    {
        public static List<CubicalMemory.Chunk_and_Cube_link> TemporalList = new List<CubicalMemory.Chunk_and_Cube_link>();
        public override void initialization()
        {
            START_initialization();

            float cx = 0, cy = 0, cz = 0;
            ShaderedScene.CalculateFromMaptoGraphical(Explosion.exp.ExplosionCenter.chunk.xz, Explosion.exp.ExplosionCenter.cube.xyz, ref cx, ref cy, ref cz);

            foreach (var item in TemporalList)
            {
                ShaderedScene.CalculateFromMaptoGraphical(item.chunk.xz, item.cube.xyz, ref x, ref y, ref z);
                Draw_Quad_Full_Sunsided_angled(x, y, z, x-cx, y-cy, z-cz, localed_range, item.cube.color, 0, true);
            }
            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
