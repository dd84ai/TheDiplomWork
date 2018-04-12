using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Keyboard
    {
        static float step = 0.1f;
        public static List<char> KeysActive = new List<char>();

        static DateTime start = DateTime.Now;
        public static void DoSpecificAction(char key)
        {
            for (int i = 0; i < 10; i++)
                    Keyboard.Wrapped_KeyPressed_Reaction(key);
        }
        public static void DoAction()
        {
            //if ((DateTime.Now - start).TotalMilliseconds > 100)
            //{
            for(int i = 0; i < 10; i++)
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

                default: break;
            }
        }
    }
}
