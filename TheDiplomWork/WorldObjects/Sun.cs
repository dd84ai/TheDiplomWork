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
            //А что это тут делает? А потому что имеет общую суть с солнцем.
            public vec3 player_pos = new vec3();
            public vec3 player_look = new vec3();
            public vec3 player_stepback = new vec3();

            public static float Sun_Height = 40.0f;
        }
        public static LocalSun S = new LocalSun();
    }
}
