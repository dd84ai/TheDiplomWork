using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    class Keyboard
    {
        public static vec4 step_vector = new vec4();
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
        public static void DoStep()
        {
            step_vector = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate(Scene.SS.env.player.coords.Player_rotational_view.x, new vec3(0.0f, 1.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f)) * step_vector;
            Scene.SS.env.player.coords.Player_precise_position.x += step_vector.x;
            Scene.SS.env.player.coords.Player_precise_position.y += step_vector.y;
            Scene.SS.env.player.coords.Player_precise_position.z += step_vector.z;
        }
        public static void Wrapped_KeyPressed_Reaction(char key)
        {
             switch (key)
            {
                case 'm': Interface.SaySomeQuote(); break;

                case 'w':
                    step_vector.x = 0; step_vector.y = 0; step_vector.z = step; DoStep(); break;
                case 's':
                    step_vector.x = 0; step_vector.y = 0; step_vector.z = -step; DoStep(); break;
                case 'a':
                    step_vector.x = step; step_vector.y = 0; step_vector.z = 0; DoStep(); break;
                case 'd':
                    step_vector.x = -step; step_vector.y = 0; step_vector.z = 0; DoStep(); break;
                case 'z':
                    step_vector.x = 0; step_vector.y = step; step_vector.z = 0; DoStep(); break;
                case ' ':
                    step_vector.x = 0; step_vector.y = -step; step_vector.z = 0; DoStep(); break;

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
