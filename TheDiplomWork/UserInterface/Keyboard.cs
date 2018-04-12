using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Keyboard
    {
        static float step = 10.0f;
        public static void Wrapped_KeyPressed_Reaction(char KeyChar)
        {
            switch (KeyChar)
            {
                case 'm': Interface.SaySomeQuote(); break;

                case 'w':
                    Scene.SS.env.player.coords.Player_precise_position.z += step; break;
                case 's':
                    Scene.SS.env.player.coords.Player_precise_position.z -= step; break;
                case 'a':
                    Scene.SS.env.player.coords.Player_precise_position.x += step; break;
                case 'd':
                    Scene.SS.env.player.coords.Player_precise_position.x -= step; break;
                case 'q':
                    Scene.SS.env.player.coords.Player_precise_position.y += step; break;
                case 'e':
                    Scene.SS.env.player.coords.Player_precise_position.y -= step; break;

                default: break;
            }
        }
    }
}
