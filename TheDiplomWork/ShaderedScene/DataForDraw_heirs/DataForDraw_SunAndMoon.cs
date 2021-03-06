﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using SharpGL;
namespace TheDiplomWork
{
    public class DataForDraw_SunAndMoon : DataForDraw_angled
    {
        public DataForDraw_SunAndMoon(OpenGL gl) : base(gl)
        {
        }
        public static mat4 SunRotator = glm.rotate(0, new vec3(0, 1, 0));

        public override void initialization()
        {
            START_initialization();

            //Sun
            Draw_Quad_Full_Sunsided_angled(
                0,
                localed_range * 10,
                0,

                0,
                 +localed_range * Sun.LocalSun.Sun_Height,
                0,

                localed_range * 10, System.Drawing.Color.Gold, 10.0f, true);

            //Moon
            Draw_Quad_Full_Sunsided_angled(
                0,
                localed_range * 10,
                0,

                0,
                 -localed_range * Sun.LocalSun.Sun_Height,
                0,

                localed_range * 10, System.Drawing.Color.WhiteSmoke, 10.0f, true);

            END_initialization();
        }
    }
}
