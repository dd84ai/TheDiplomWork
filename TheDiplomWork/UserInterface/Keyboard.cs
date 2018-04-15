using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Windows.Forms;
namespace TheDiplomWork
{
    class Keyboard
    {
        public static vec4 step_vector = new vec4();
        public static float step = 0.25f * CubicalMemory.Cube.rangeOfTheEdge;
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
        public static void RepeatLastStep()
        {
            DoStep(step_vector);
        }
        public static void DoStep(vec4 MoveVector)
        {
            for (int i = 0; i < 4; i++)
            {
                Wrapped_Do_Step(MoveVector, ref Scene.SS.env.player.coords.Player_precise_position);
                
            }
        }
        public static void Wrapped_Do_Step(vec4 _step, ref GeneralProgrammingStuff.Point3D WhatToMove, bool SensetivityToY = false)
        {
            vec4 step_vector_extra;

            if (!SensetivityToY)
            step_vector_extra = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate(Scene.SS.env.player.coords.Player_rotational_view.x, new vec3(0.0f, 1.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f)) * _step;
            else
            step_vector_extra = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate(Scene.SS.env.player.coords.Player_rotational_view.x, new vec3(0.0f, -1.0f, 0.0f)) * glm.rotate(-Scene.SS.env.player.coords.Player_rotational_view.y, new vec3(1.0f, 0.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, -1.0f)) * _step; //* glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f)) 8* _step;
            WhatToMove.x -= step_vector_extra.x;
            WhatToMove.y -= step_vector_extra.y;
            WhatToMove.z -= step_vector_extra.z;
        }
        public static void Wrapped_KeyPressed_Reaction(char key)
        {
             switch (key)
            {
                case 'm': Interface.SaySomeQuote(); break;

                case 'w':
                    step_vector.x = 0; step_vector.y = 0; step_vector.z = step; DoStep(step_vector); break;
                case 's':
                    step_vector.x = 0; step_vector.y = 0; step_vector.z = -step; DoStep(step_vector); break;
                case 'a':
                    step_vector.x = step; step_vector.y = 0; step_vector.z = 0; DoStep(step_vector); break;
                case 'd':
                    step_vector.x = -step; step_vector.y = 0; step_vector.z = 0; DoStep(step_vector); break;
                case 'z':
                    step_vector.x = 0; step_vector.y = step; step_vector.z = 0; DoStep(step_vector); break;
                case ' ':
                    step_vector.x = 0; step_vector.y = -step; step_vector.z = 0; DoStep(step_vector); break;

                case 'l':
                    Scene.SS.env.player.coords.Player_rotational_view.x += rotational_step; break;
                case 'j':
                    Scene.SS.env.player.coords.Player_rotational_view.x -= rotational_step; break;
                case 'i':
                    Scene.SS.env.player.coords.Player_rotational_view.y += rotational_step; break;
                case 'k':
                    Scene.SS.env.player.coords.Player_rotational_view.y -= rotational_step; break;

                case 'q':
                    Application.Exit(); break;

                case 'r':
                    Scene.SS.env.player.coords.Player_precise_position.Save("PlayerPosition");
                    Scene.SS.env.player.coords.Player_rotational_view.Save("PlayerRotationalView");
                    Application.Restart();
                    Application.Exit(); break;
                case 'c':
                    Scene.SS.env.player.coords.Player_precise_position.x = Scene.SS.env.player.coords.Player_Default_position.x;
                    Scene.SS.env.player.coords.Player_precise_position.y = Scene.SS.env.player.coords.Player_Default_position.y;
                    Scene.SS.env.player.coords.Player_precise_position.z = Scene.SS.env.player.coords.Player_Default_position.z;
                    Scene.SS.env.player.coords.Player_rotational_view = new GeneralProgrammingStuff.Point3D(3.14f / 2f, 0, 0);
                    break;

                default: break;
            }
        }
    }
}
