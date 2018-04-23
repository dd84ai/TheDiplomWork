using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
namespace TheDiplomWork
{
    public class DataForDraw_SunAndMoon : DataForDraw_angled
    {
        public static int Time = 0;
        public static mat4 SunRotator = glm.rotate((float) Time / 100, new vec3(1, 0, 0)) * glm.rotate((float) Time / 100, new vec3(0, 1, 0));
        public override void initialization()
        {
            START_initialization();
            vec4 Position;

            Position = new vec4(Scene.SS.env.player.coords.Player_precise_position.x,
                Scene.SS.env.player.coords.Player_precise_position.y + localed_range * 100,
                Scene.SS.env.player.coords.Player_precise_position.z,1.0f);

            SunRotator = glm.rotate((float)Time / 100, new vec3(1, 0, 0)) * glm.rotate((float)Time / 100, new vec3(0, 1, 0));
            Position = SunRotator * Position;

            //Sun
            Draw_Quad_Full_Sunsided_angled(
                Position.x,
                Position.y,
                Position.z,

                Scene.SS.env.player.coords.Player_precise_position.x,
                Scene.SS.env.player.coords.Player_precise_position.y,
                Scene.SS.env.player.coords.Player_precise_position.z,

                localed_range * 10, System.Drawing.Color.Gold, 0.0f, 0.0f, 0.0f, true);

            //Moon
            Draw_Quad_Full_Sunsided_angled(
                Scene.SS.env.player.coords.Player_precise_position.x,
                Scene.SS.env.player.coords.Player_precise_position.y - localed_range * 100,
                Scene.SS.env.player.coords.Player_precise_position.z,

                Scene.SS.env.player.coords.Player_precise_position.x,
                Scene.SS.env.player.coords.Player_precise_position.y,
                Scene.SS.env.player.coords.Player_precise_position.z,

                localed_range * 10, System.Drawing.Color.WhiteSmoke, 0.0f, 0.0f, 0.0f, true);

            END_initialization();
        }
    }
}
