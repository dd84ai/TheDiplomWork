using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_Projectile : DataForDraw_angled
    {
        public DataForDraw_Projectile(OpenGL gl) : base(gl)
        {
        }
        public override void initialization()
        {
            START_initialization();

            foreach (var cube in Projectile.jp.ProjectileParts)
            {
                ShaderedScene.CalculateFromMaptoGraphical(cube, ref x, ref y, ref z);
                Draw_Quad_Full_Sunsided_angled(x, y, z, cube.FallingStartingTime, cube.FallingFromHeight * (CubicalMemory.Cube.rangeOfTheEdge), 0.0f, localed_range, cube.color, 0, true);
            }

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
