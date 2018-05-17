using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_FreshlyPlacedCubes : DataForDraw_angled
    {
        public DataForDraw_FreshlyPlacedCubes(OpenGL gl) : base(gl)
        {
        }
        public static List<CubicalMemory.Cube> TemporalList = new List<CubicalMemory.Cube>();
        public override void initialization()
        {
            START_initialization();

            foreach (var cube in TemporalList)
            {
                ShaderedScene.CalculateFromMaptoGraphical(cube, ref x, ref y, ref z);
                Draw_Quad_Full_Sunsided_angled(x, y, z, cube.FallingStartingTime, cube.FallingFromHeight * (CubicalMemory.Cube.rangeOfTheEdge), 0.0f, localed_range, cube.color, 0, true);
            }

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
