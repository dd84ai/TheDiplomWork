using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
namespace TheDiplomWork
{
    public class FromShaderWithLove
    {
        public class ShaderRotator
        {
            vec3 in_Position;
            public ShaderRotator(vec3 inp)
            {
                in_Position = inp;
            }
            vec3 Rotate(vec3 SomethingToRotate, vec3 ang)
            {
                mat3 RotateX = new mat3(new vec3(1, 0, 0),
                new vec3(0, (float)Math.Cos(ang.x), -(float)Math.Sin(ang.x)),
                new vec3(0, (float)Math.Sin(ang.x), (float)Math.Cos(ang.x)));

                mat3 RotateY = new mat3(new vec3((float)Math.Cos(ang.y), 0, (float)Math.Sin(ang.y)),
                new vec3(0, 1, 0),
                new vec3(-(float)Math.Sin(ang.y), 0, (float)Math.Cos(ang.y)));

                mat3 RotateZ = new mat3(new vec3((float)Math.Cos(ang.z), -(float)Math.Sin(ang.z), 0),
                new vec3((float)Math.Sin(ang.z), (float)Math.Cos(ang.z), 0),
                new vec3(0, 0, 1));
                mat3 Rotator = RotateX * (RotateY * (RotateZ));

                vec3 temp = Rotator * SomethingToRotate;
                return temp;
            }
            vec3 Shifted(vec3 input_vec)
            {
                return new vec3(input_vec + (in_Position));
            }
            vec3 Deshifted(vec3 input_vec)
            {
                return new vec3(input_vec - (in_Position));
            }
            vec3 ProcessingProjectile(vec3 inp)
            {
                //Rotate
                vec3 ShiftToRotate = Shifted(inp) - Projectile.jp.center;
                ShiftToRotate = Rotate(ShiftToRotate, Projectile.jp.PlayerAngles());
                //ShiftToRotate = Deshifted(ShiftToRotate) + Projectile.jp.center;

                vec3 ShiftedToRelativePosition = Projectile.jp.Coordinates() + ShiftToRotate;

                return ShiftedToRelativePosition;
            }
            public vec3 ReturnTheThing() { return ProcessingProjectile(new vec3(0, 0, 0)); }
        }
    }
}
