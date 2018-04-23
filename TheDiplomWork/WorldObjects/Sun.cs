using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class Sun
    {
        public class LocalSun
        {
            public vec3 player_pos = new vec3();
            public vec3 player_look = new vec3();
            public vec3 player_stepback = new vec3();

            //public mat4 sunMatrix = new mat4();
            //Так как солнце влияет на основную модель. Значит логично строить его там. Чего не хватает?
            //[0] x,y,z - position, w - rotate angle?
            //[1]r,g,b - it's own brightness, w - shared brightness
            //[2]r,g,b - it's own color? w - shared brightness
            //[3]x,y,z - rotated around itself;
            //[3][3] bool thing to enable Sun Shader in Main Shader;

            //Что нужно для солнца? У него будет
            /*
             * Mat4 под солнце.
             * x,y,z его позиции. = Игрок + по игреку 100.
             * r,g,b его яркость. = Светить как солнце иль как луна
             * r,g,b его цвет. //не нужно?
             * время?Так то чеез него выразить ротэйтор сразу в шейдер.
             */
        }
        public static LocalSun S = new LocalSun();
    }
}
