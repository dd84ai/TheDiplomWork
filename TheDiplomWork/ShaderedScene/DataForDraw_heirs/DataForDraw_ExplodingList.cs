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
        public static List<CubicalMemory.Cube> TemporalList_ToClear = new List<CubicalMemory.Cube>();
        public static void TemporalList_Save()
        {
            foreach (var item in TemporalList) TemporalList_ToClear.Add(item);
        }
        public static void TemporalList_Clear()
        {
            foreach (var cube in DataForDraw_ExplodingList.TemporalList_ToClear)
            {
                cube.IsTakenForExplosion = false;
                cube.FallingStartingTime = 0;
            }
            TemporalList_ToClear.Clear();
        }
        public override void initialization()
        {
            START_initialization();

            
            float x = 0, y = 0, z = 0;
            if (!Projectile.jp.Loaded)
                ShaderedScene.CalculateFromMaptoGraphical(Explosion.exp.ExplosionCenter, ref x, ref y, ref z);
            else
            {
                x = Projectile.jp.AbsoluteEstimatedLocation().x;
                y = Projectile.jp.AbsoluteEstimatedLocation().y;
                z = Projectile.jp.AbsoluteEstimatedLocation().z;
            }


            ForEachTemporalList(this,TemporalList,x,y,z);

            //if (Projectile.jp.Loaded)
            //    ForEachTemporalList(Projectile.jp.ProjectileParts);


            END_initialization();
            base.LastCount = vertices.Count();
        }
        public static double ExplosionVelocity = 0;
        public static void ForEachTemporalList(DataForDraw_angled Data, List<CubicalMemory.Cube> Temper, float cx, float cy, float cz, bool ShowAlways = false)
        {
            float x = 0, y = 0, z = 0;
            
            //Так по поводу взрыва. Давай все мерить в TNT эквиваленте.
            float Me = (float)Projectile.settings.Me; //mass of the fragmenting casing
            float Mc = (float)Projectile.settings.Mc; //mass of the explosive charge
            float K = (float)Projectile.settings.K; //Geometrical Constant for cube
            float dE = (float)Projectile.settings.dE; // J/kg Heat of TNT Explosion

            double rightpart = (Mc / Me) / (1 + K * (Mc / Me));

            float V = (float)Math.Sqrt(2 * dE * (rightpart));
            ExplosionVelocity = V;
            foreach (var cube in Temper)
            {
                ShaderedScene.CalculateFromMaptoGraphical(cube, ref x, ref y, ref z);

                float Vx = x - cx, Vy = y - cy, Vz = z - cz;
                float Range = (float)Math.Sqrt((double)Vx * Vx + Vy * Vy + Vz * Vz);
                Vx *= V / (Range * Range); Vy *= V / (Range * Range); Vz *= V / (Range * Range);
                float Velocity = (float)Math.Sqrt((double)Vx * Vx + Vy * Vy + Vz * Vz);

                if (ShowAlways || Velocity > 10)
                {
                    cube.IsTakenForExplosion = true;
                    Data.Draw_Quad_Full_Sunsided_angled(x, y, z, Vx, Vy, Vz, localed_range, cube.color, 0, true);
                }
            }
        }
    }
}
