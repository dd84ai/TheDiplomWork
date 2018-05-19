using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
namespace TheDiplomWork
{
    class DataForDraw_TrajectoryPath : DataForDraw_angled
    {
        public DataForDraw_TrajectoryPath(OpenGL gl) : base(gl)
        {
        }
        public override void initialization()
        {
            START_initialization();

            float EndTime = Projectile.jp.TimeWhenSecondZero();
            vec3 temp;
            //if (!Projectile.jp.NewVersion)
            //{
                
                int count = 4 * (int)EndTime + 4*2;
                
                for (int i = 2; i <= count; i++)
                {
                    temp = Projectile.jp.AbsoluteLocationAtTime((float)i * EndTime / count);
                    Draw_Quad_Full_Sunsided_angled(temp.x, temp.y, temp.z, 0, 0, 0, localed_range, System.Drawing.Color.Black, 0, true);
                }
            //}
            //else
            //{
            //    temp = Projectile.jp.AbsoluteLocationAtTime((float)EndTime * 0.1f);
            //    Draw_Quad_Full_Sunsided_angled(temp.x, temp.y, temp.z, 0, 0, 0, localed_range, System.Drawing.Color.Black, 0, true);

            //    temp = Projectile.jp.AbsoluteLocationAtTime((float)EndTime * 0.2f);
            //    Draw_Quad_Full_Sunsided_angled(temp.x, temp.y, temp.z, 0, 0, 0, localed_range, System.Drawing.Color.Black, 0, true);
            //}

            //foreach (var cube in TemporalList)
            //{
            //    ShaderedScene.CalculateFromMaptoGraphical(cube, ref x, ref y, ref z);
            //    Draw_Quad_Full_Sunsided_angled(x, y, z, cube.FallingStartingTime, cube.FallingFromHeight * (CubicalMemory.Cube.rangeOfTheEdge), 0.0f, localed_range, cube.color, 0, true);
            //}

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
