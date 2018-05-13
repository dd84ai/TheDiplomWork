using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class DataForDraw_ExplodingList : DataForDraw_angled
    {
        public DataForDraw_ExplodingList(OpenGL gl) : base(gl)
        {
        }
        public static List<CubicalMemory.Cube> TemporalList = new List<CubicalMemory.Cube>();
        public override void initialization()
        {
            START_initialization();

            float cx = 0, cy = 0, cz = 0;
            ShaderedScene.CalculateFromMaptoGraphical(Explosion.exp.ExplosionCenter, ref cx, ref cy, ref cz);

            //Так по поводу взрыва. Давай все мерить в TNT эквиваленте.
            float Me = 1.0f; //mass of the explosive charge
            float Mc = 1.0f; //mass of the fragmenting casing
            float K = 1 / 2; //Geometrical Constant
            float dE = 2.157e+6f; // J/kg Heat of TNT Explosion

            float V = (float)Math.Sqrt(2 * dE * ((Mc / Me) / (1 + K * (Mc / Me)))) / 10.0f;

            foreach (var cube in TemporalList)
            {
                ShaderedScene.CalculateFromMaptoGraphical(cube, ref x, ref y, ref z);

                float Vx = x - cx, Vy = y - cy, Vz = z - cz;
                float Range = (float)Math.Sqrt((double)Vx * Vx + Vy * Vy + Vz * Vz);
                Vx *= V / (Range * Range); Vy *= V / (Range * Range); Vz *= V / (Range * Range);
                float Velocity = (float)Math.Sqrt((double)Vx * Vx + Vy * Vy + Vz * Vz);

                if (Velocity > 10)
                {
                    cube.IsTakenForExplosion = true;
                    Draw_Quad_Full_Sunsided_angled(x, y, z, Vx, Vy, Vz, localed_range, cube.color, cube.FallingStartingTime, true);
                }
            }
            

            END_initialization();
            base.LastCount = vertices.Count();
        }
    }
}
