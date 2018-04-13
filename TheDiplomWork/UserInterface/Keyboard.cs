using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Keyboard
    {
        static float step = 1.0f;
        static float rotational_step = 0.05f;
        public static List<char> KeysActive = new List<char>();

        static DateTime start = DateTime.Now;
        public static void DoSpecificAction(char key)
        {
                    Keyboard.Wrapped_KeyPressed_Reaction(key);
        }
        public static void DoAction()
        {
            //if ((DateTime.Now - start).TotalMilliseconds > 100)
            //{
                foreach (var key in Keyboard.KeysActive)
                    Keyboard.Wrapped_KeyPressed_Reaction(key);
                //start = DateTime.Now;
            //}
        }
        public static void Wrapped_KeyPressed_Reaction(char key)
        {
             switch (key)
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
                case 'z':
                    Scene.SS.env.player.coords.Player_precise_position.y += step; break;
                case ' ':
                    Scene.SS.env.player.coords.Player_precise_position.y -= step; break;

                case 'l':
                    Scene.SS.env.player.coords.Player_rotational_view.x += rotational_step; break;
                case 'j':
                    Scene.SS.env.player.coords.Player_rotational_view.x -= rotational_step; break;
                case 'i':
                    Scene.SS.env.player.coords.Player_rotational_view.y += rotational_step; break;
                case 'k':
                    Scene.SS.env.player.coords.Player_rotational_view.y -= rotational_step; break;

                default: break;
            }
        }
    }
}
